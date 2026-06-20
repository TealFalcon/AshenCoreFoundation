using UnityEngine;

namespace AshenCore.Core
{

    public static class ACMath
    {

        public static float RandomGaussian(float mean, float standardDeviation)
        {

            float u1 = 1.0f - Random.value;
            float u2 = 1.0f - Random.value;

            float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) *
                          Mathf.Sin(2.0f * Mathf.PI * u2);

            return mean + standardDeviation * randStdNormal;

        }


    }

}