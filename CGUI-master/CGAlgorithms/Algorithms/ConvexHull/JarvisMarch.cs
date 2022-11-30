using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class JarvisMarch : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            
            int numOfPoints = points.Count;

            // If Number of points is less than three return points as a Extreme Point.
            if (numOfPoints < 3)
            {
                outPoints = points;
                return;
            }

            //Get minimum Y 
            int Ymin = 0;
            for (int i = 1; i < numOfPoints; i++)  
                if (points[i].Y < points[Ymin].Y)
                    Ymin = i;

            // Start point is the Y minimum
            int start = Ymin;

            // Adding start point in Out Points
            outPoints.Add(points[start]);
            double x_ = points[start].X;
            double y_ = points[start].Y;
            points.RemoveAt(start);

            int idxOfMinPoint = -1;
            //bool sTrue = false;
            while (true)
            {
                double Theta_min = 360;
                for (int i = 0; i < points.Count; i++)
                {
                    double Dx = points[i].X + x_;
                    double Dy = points[i].Y + y_;
                    double Theta = Math.Atan(Dy / Dx) * (180 / Math.PI);

                    if (Theta < 0)
                        Theta = 180 + Theta;

                    if(Theta < Theta_min)
                    {
                        Theta_min = Theta;
                        idxOfMinPoint = i;
                    }
                    
                }
                x_ = points[idxOfMinPoint].X;
                y_ = points[idxOfMinPoint].Y;     
                outPoints.Add(points[idxOfMinPoint]);
                points.RemoveAt(idxOfMinPoint);

                

                // After Removing Start Point the Indexes are Changed.
                // So We need to find the new start point.
                for (int i = 0; i < points.Count; i++)
                {
                    if (points[i].X == x_ && points[i].Y == y_)
                        start = i;
                }

                if (points.Count <= 0)
                    break;
            }

        }

        public override string ToString()
        {
            return "Convex Hull - Jarvis March";
        }
    }
}
