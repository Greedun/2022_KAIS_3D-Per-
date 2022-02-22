using System;
using System.Collections.Generic;
using System.Text;

namespace Point
{
    public class Point
    {
        int func_incline;
        int func_yintercept;
        int x;
        int y;
        int third_location_x;
        int third_location_y;
        // location에는 3개의 점이 들어온다고 가정
        // 첫번째와 두번째 점에는 함수(선)를 생성, 3번째 점과 선으로 거리 계산
        public static double PointToLine(double[] p1, double[] p2, double[] p3)
        {
            double l1, l2, l3; //점사이의 거리 3개
            double linedis; //점과 선사이의 거리
            double tri_area;
            double s;

            l1 = Distance_Point_to_Point(p1, p2);
            l2 = Distance_Point_to_Point(p2, p3);
            l3 = Distance_Point_to_Point(p3, p1);
            s = (l1 + l2 + l3) / 2;

            tri_area = Math.Sqrt(s * (s - l1) * (s - l2) * (s - l3));
            linedis = 2 * tri_area / l1;
            linedis = Math.Round(linedis, 4);

            return linedis;
        }

        // 두 점사이의 거리 구하는 함수
        public static double Distance_Point_to_Point(double[] p1, double[] p2)
        {
            double pointdis = 0;
            pointdis = Math.Sqrt((Math.Pow(p2[0] - p1[0], 2) + Math.Pow(p2[1] - p1[1], 2) + Math.Pow(p2[2] - p1[2], 2)));

            pointdis = Math.Round(pointdis, 4);

            return pointdis;
        }
    }
}
