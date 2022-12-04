using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class GrahamScan : Algorithm
    {
        Stack<Point> l = new Stack<Point>();
        List < KeyValuePair<int, double> > angles = new List< KeyValuePair<int, double> >(); // pointIndex, Angle
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            Point minY = findMinimumY(points);
            l.Push(minY);
            points.Remove(minY);

            getAngles(minY, points);
            angles.Sort((x,y) => x.Value.CompareTo(y.Value));

            l.Push(points[angles[0].Key]);
            for(int i = 1; i < angles.Count;)
            {
                Point p = l.Peek();
                Point p_ = Previous();

                Enums.TurnType turnTest = HelperMethods.CheckTurn(new Line(p_, p), points[angles[i].Key]);
                if(turnTest == Enums.TurnType.Left)
                {
                    l.Push(points[angles[i].Key]);
                    i++;
                }
                else
                {
                    l.Pop();
                }
            }

            getOutput(ref outPoints);

        }

        private Point Previous()
        {
            Point top = l.Pop();
            Point prev = l.Peek();
            l.Push(top);
            return prev;
        }
        private Point findMinimumY(List<Point> points)
        {
            int minimumYIdx = 0;

            for(int i = 0; i < points.Count; i++)
            {
                if (points[i].Y <= points[minimumYIdx].Y)
                    minimumYIdx = i;           
            }

            return points[minimumYIdx];
        }

        private void getAngles(Point v, List<Point>points)
        {
            Line l1 = new Line(v, new Point(v.X + 10.0, v.Y));

            for (int i =0; i< points.Count; i++)
            {
                Line l2 = new Line(v, points[i]);

                Point vector1 = HelperMethods.GetVector(l1);
                Point vector2 = HelperMethods.GetVector(l2);

                double angle = Math.Atan2(HelperMethods.CrossProduct(vector1,vector2), HelperMethods.DotProduct(vector1,vector2)) * (180.0 / Math.PI);
                angles.Add(new KeyValuePair<int, double>(i, angle));
            }
          
        }

        public void printAngles()
        {
            foreach(var i in angles)
            {
                Console.WriteLine(i.Key + " " + i.Value);
            }
        }

        public void printStack()
        {
            while(l.Count != 0)
            {
                Point p = l.Pop();
                Console.WriteLine(p.X + " " + p.Y);
            }
        }

        public void getOutput(ref List<Point> outPoints)
        {
            while(l.Count != 0)
            {
                outPoints.Add(l.Pop());
            }
        }

        public override string ToString()
        {
            return "Convex Hull - Graham Scan";
        }
    }
}
