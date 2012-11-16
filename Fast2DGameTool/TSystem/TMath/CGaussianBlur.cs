using System;
using System.Collections.Generic;

namespace Tool.TSystem.TMath
{
    public struct FilterKernelElement
    {
        public float du;
        public float dv;
        public float Weight;
    };

    public struct GaussianProperties
    {
        public bool Disabled;
        public float RadiusScale;
        public float Amp;
    };


    public class CGaussianBlur
    {
        public int Diameter;

        public bool RemoveSmallContributors;
        public List<FilterKernelElement> Kernel;
        public GaussianProperties[] Properties;
        public float totalWeight;

        public CGaussianBlur(int diam, GaussianProperties[] props, bool removeSmallContribs)
        {
            Diameter = diam;
            RemoveSmallContributors = removeSmallContribs;

            Properties = new GaussianProperties[props.Length];
            props.CopyTo(Properties, 0);

            Kernel = new List<FilterKernelElement>();
        }

        #region Methods
        public bool GenerateKernel()
        {
            FilterKernelElement el = new FilterKernelElement();

            int u;

            float w = 0;

            float weight;
            float radi;
            float center = (((float)Diameter) - 1.0f) / 2.0f;

            // Create a 1D gaussian kernel to create a 2D "blur" filter

            Kernel.Clear();

            int m;

            for (u = 0; u < Diameter; u++)
            {
                el.du = ((float)u) - center;
                el.dv = 0.0f;

                radi = el.du * el.du / (center * center);

                // Apply the gaussian function
                el.Weight = 0.0f;
                for (m = 0; m < Properties.Length; m++)
                {
                    GaussianProperties prop = Properties[m];
                    if (!prop.Disabled)
                    {
                        weight = radi * prop.RadiusScale;
                        weight = (float)(1.0f / Math.Exp(weight));
                        el.Weight += weight * prop.Amp;

                        w += el.Weight;
                    }
                }

                // Only add the elements that contribute more than 1%
                if (((el.Weight > 0.01f) && RemoveSmallContributors) || !RemoveSmallContributors)
                    Kernel.Add(el);
            }

            totalWeight = w;

            //Repository.GetInstance().AddDebug("The total diameter for the Gaussian Blur is  " + Diameter.ToString() + " and the weight is " + w.ToString());

            return true;
        }

        #endregion
    }
}
