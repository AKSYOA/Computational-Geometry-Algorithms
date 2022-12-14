using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class QuickHull : Algorithm
    {
        public List<Point> quickHull(List<Point> points, Point min_x, Point max_x, string direction)
        {
            var segment = Enums.TurnType.Left;
            if (direction == "Right")
            {
                segment = Enums.TurnType.Right;
            }
            else
            {
                segment = Enums.TurnType.Left;
            }
            int index = -1;
            double max = -1;
            List<Point> point = new List<Point>();
            if (points.Count == 0)
                return point;
            for (int i = 0; i < points.Count; i++)
            {
                double x = distence(min_x, max_x, points[i]);
                if (CGUtilities.HelperMethods.CheckTurn(new Line(min_x.X, min_x.Y, max_x.X, max_x.Y), points[i]) == segment && x > max)
                {
                    index = i;
                    max = x;
                }
            }
            if (index == -1)
            {
                point.Add(min_x);
                point.Add(max_x);

                return point;
            }
            List<Point> p1, p2;
            if (CGUtilities.HelperMethods.CheckTurn(new Line(points[index].X, points[index].Y, min_x.X, min_x.Y), max_x) ==
                Enums.TurnType.Right)
            {
                p1 = quickHull(points, points[index], min_x, "Left");
            }
            else
            {
                p1 = quickHull(points, points[index], min_x, "Right");

            }

            if (CGUtilities.HelperMethods.CheckTurn(new Line(points[index].X, points[index].Y, max_x.X, max_x.Y), min_x) ==
                Enums.TurnType.Right)
            {
                p2 = quickHull(points, points[index], max_x, "Left");

            }
            else
            {
                p2 = quickHull(points, points[index], max_x, "Right");

            }
            for (int i = 0; i < p2.Count; i++)
                p1.Add(p2[i]);
            return p1;
        }
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            Point max_x = new Point(-100000, 0);
            Point min_x = new Point(100000, 0);
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].X < min_x.X)
                    min_x = points[i];
                if (points[i].X > max_x.X)
                    max_x = points[i];
            }
            List<Point> right = quickHull(points, min_x, max_x, "Right");
            List<Point> left = quickHull(points, min_x, max_x, "Left");
            for (int i = 0; i < left.Count; i++)
                right.Add(left[i]);

            for (int i = 0; i < right.Count; i++)
            {
                if (!outPoints.Contains(right[i]))
                    outPoints.Add(right[i]);
            }
        }
        public double distence(Point first, Point second, Point third)
        {
            return Math.Abs((third.Y - first.Y) * (second.X - first.X) -
                       (second.Y - first.Y) * (third.X - first.X));
        }
        public override string ToString()
        {
            return "Convex Hull - Quick Hull";
        }
    }
}
