using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

namespace objFile_Render
{
    public class objFile_Render
    {
        public static float[,] ReadXyz()
        {
            string path = @"C:\Users\soyen\Desktop\2022_winter(M)\Personal\github\2022_KAIS_3Drendering\3d_rendering\test3.asc";
            string[] textValue = File.ReadAllLines(path);
            float[, ] xyz = new float[textValue.Length, 3];
           
            for (int i = 0; i < textValue.Length; i++)
            {
                try
                {
                    string[] SpaceSplit = textValue[i].Split(' ');
                    for (int j = 0; j < SpaceSplit.Length; j++)
                    {
                        if (j < 3)
                        {
                            xyz[i, j] = float.Parse(SpaceSplit[j]);
                        }
                    }
                }
                catch {; }
            }

            return xyz;
            // modified test
        }

        public static int[,] ReadRgb()
        {
            string path = @"C:\Users\soyen\Desktop\2022_winter(M)\Personal\github\2022_KAIS_3Drendering\3d_rendering\test3.asc";
            string[] textValue = File.ReadAllLines(path);
            int[,] rgb = new int[textValue.Length, 3];
            
            for (int i = 0; i < textValue.Length; i++)
            {
                try
                {
                    string[] SpaceSplit = textValue[i].Split(' ');
                    for (int j = 0; j < SpaceSplit.Length; j++)
                    {
                        if (j > 2)
                        {
                            rgb[i, j - 3] = int.Parse(SpaceSplit[j]);
                        }
                    }
                }
                catch {; }
            }

            return rgb;
        }

        public static void xyzrgb_Render(float[,] xyz_arr, int[,] rgb_arr)
        {
            int xyz_l = (xyz_arr.Length) / 3;
            int rgb_l = (rgb_arr.Length) / 3;

            // floating오차 해결 방법 찾아보기
            for (int i = 0; i < rgb_l; i++)
            {
                GL.Color4(Color4.Silver);
                GL.Vertex3(xyz_arr[i, 0], xyz_arr[i, 1], xyz_arr[i, 2]);
            }
        }
    }
}
