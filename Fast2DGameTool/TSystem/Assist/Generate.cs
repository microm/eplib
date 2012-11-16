using System;
using System.Collections.Generic;
using System.Diagnostics;
using Tool.TSystem.Basis;
using Tool.TSystem.Primitive;
using Tool.TSystem.TMath;

namespace Tool.TSystem.Assist
{
    public static class Generate
    {

        public static float[,] GetPerlinNoise(int width)
        {
            Trace.Assert(IsMultipleOf2(width));

            float persistence = 2.5f;

            return GetPerlinNoise(width, 1, 64, 0, 1, persistence);
        }

        public static float[,] GetPerlinNoise(int width,
                                              int minWaveLength,
                                              int maxWaveLength,
                                              float minValue,
                                              float maxValue,
                                              float persistence)
        {
            Trace.Assert(minWaveLength < maxWaveLength &&
                         IsMultipleOf2(minWaveLength) &&
                         IsMultipleOf2(maxWaveLength));

            Random random = new Random((int)new Timer().GetTime());
            List<float[,]> octaveNoise = new List<float[,]>();

            float amplitude = 1;
            for (int waveLength = maxWaveLength; waveLength >= minWaveLength; waveLength /= 2)
            {
                octaveNoise.Add(GetNoise(width, waveLength, amplitude, random));

                amplitude /= persistence;
            }

            float[,] perlinNoise = new float[width, width];
            foreach (float[,] noise in octaveNoise)
            {
                for (int y = 0; y < width; ++y)
                {
                    for (int x = 0; x < width; ++x)
                    {
                        perlinNoise[y, x] += (noise[y, x] / 2);
                    }
                }
            }

            for (int y = 0; y < width; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    float noise = perlinNoise[y, x];
                    perlinNoise[y, x] = NormalizeNoise(noise) * (maxValue - minValue) + minValue;
                }
            }

            return perlinNoise;
        }

        private static float NormalizeNoise(float noise)
        {
            return noise * 0.5f + 0.5f;
        }

        private static bool IsMultipleOf2(int number)
        {
            return (int)Math.Pow(2, (int)Math.Log(number, 2)) == number;
        }

        private static float[,] GetNoise(int width, int waveLength, float amplitude, Random random)
        {
            float[,] noise = new float[width + 1, width + 1];
            for (int y = 0; y < width + 1; y += waveLength)
            {
                for (int x = 0; x < width + 1; x += waveLength)
                {
                    float floatRandom = (float)random.NextDouble() * 2 - 1;
                    noise[y, x] = amplitude * floatRandom;
                }
            }

            if (waveLength > 1)
            {
                InterpolateXAxis(width, noise, waveLength);
                InterpolateYAxis(width, noise, waveLength);
            }
            return noise;
        }

        private static void InterpolateYAxis(int width, float[,] noise, int waveLength)
        {
            for (int y = 0; y < width; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    if (y % waveLength == 0) continue;

                    int upY = (y / waveLength) * waveLength;

                    float up = noise[upY, x];
                    float down = noise[upY + waveLength, x];

                    float weight = (y - upY) / (float)waveLength;
                    weight = ConvertToEasyCurveWeight(weight);
                    noise[y, x] = Common.Lerp(up, down, weight);
                }
            }
        }

        private static void InterpolateXAxis(int width, float[,] noise, int waveLength)
        {
            //X축보간은 현재 Y축 waveLength Line에만 값이 있으므로, 그 Line에서만 수행한다.
            for (int y = 0; y < width + 1; y += waveLength)
            {
                for (int x = 0; x < width; ++x)
                {
                    if (x % waveLength == 0) continue;

                    int leftX = (x / waveLength) * waveLength;

                    float left = noise[y, leftX];
                    float right = noise[y, leftX + waveLength];

                    float weight = (x - leftX) / (float)waveLength;
                    weight = ConvertToEasyCurveWeight(weight);
                    noise[y, x] = Common.Lerp(left, right, weight);

                }
            }
        }

        public static float ConvertToEasyCurveWeight(float linearWeight)
        {
            return 3 * linearWeight * linearWeight - 2 * linearWeight * linearWeight * linearWeight;
        }

    }
}
