using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities.DataStructures;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class Incremental : Algorithm
    {

        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            if (points.Count < 3)
            {
                outPoints = points;
                return;
            }
            OrderedSet<Tuple<double, int>> All = new OrderedSet<Tuple<double, int>>(); //all points  
            Point Mid = new Point((points[0].X + points[1].X) / 2, (points[0].Y + points[1].Y) / 2);

            Mid.X = (Mid.X + points[2].X) / 2;
            Mid.Y = (Mid.Y + points[2].Y) / 2;

            Point NextP = new Point(Mid.X, Mid.Y); 
            Line SupportingLine = new Line(Mid, NextP);
            for (int i = 0; i < 3; i++)
            {
                double angle = Check(SupportingLine.Start, SupportingLine.End, SupportingLine.Start, points[i]);
                All.Add(new Tuple<double, int>(angle, i));//add point (angle,index)
            }

            for (int i = 3; i < points.Count; i++)
            {
                KeyValuePair<Tuple<double, int>, Tuple<double, int>> Data = new KeyValuePair<Tuple<double, int>, Tuple<double, int>>();
                double angle = Check(SupportingLine.Start, SupportingLine.End, Mid, points[i]);
                Data = All.DirectUpperAndLower(new Tuple<double, int>(angle, i));
                Tuple<double, int> Next = null;
                Next = Data.Key;//next point
                Tuple<double, int> Previous = null;
                Previous = Data.Value;//previous point
                if (Previous == null)
                {
                    Previous = All.GetLast();
                }
                if (Next == null)
                {
                    Next = All.GetFirst();
                }
                Line line = new Line(points[Previous.Item2], points[Next.Item2]);
                Enums.TurnType type = HelperMethods.CheckTurn(line, points[i]);

                if (type == Enums.TurnType.Right)
                {
                    Tuple<double, int> Previous1 = null;
                    Data = All.DirectUpperAndLower(Previous);
                    Previous1 = Data.Value;

                    if (Previous1 == null)
                    {
                        Previous1 = All.GetLast();
                    }

                    line = new Line(points[i], points[Previous.Item2]);
                    type = HelperMethods.CheckTurn(line, points[Previous1.Item2]);

                    while (type == Enums.TurnType.Left || type == Enums.TurnType.Colinear) //No SupportingLine
                    {
                        All.Remove(Previous);
                        Previous = Previous1;
                        Data = All.DirectUpperAndLower(Previous);
                        Previous1 = Data.Value;
                        if (Previous1 == null)
                        {
                            Previous1 = All.GetLast();
                        }
                        line = new Line(points[i], points[Previous.Item2]);
                        type = HelperMethods.CheckTurn(line, points[Previous1.Item2]);
                    }

                    //Console.WriteLine(Data);

                    Tuple<double, int> Next1 = null;
                    Data = All.DirectUpperAndLower(Next);
                    Next1 = Data.Key;
                    if (Next1 == null)
                    {
                        Next1 = All.GetFirst();
                    }
                    line = new Line(points[i], points[Next.Item2]);
                    type = HelperMethods.CheckTurn(line, points[Next1.Item2]);

                    while (type == Enums.TurnType.Right || type == Enums.TurnType.Colinear) 
                    {
                        All.Remove(Next);
                        Next = Next1;
                        Data = All.DirectUpperAndLower(Next);
                        Next1 = Data.Key;
                        if (Next1 == null)
                        {
                            Next1 = All.GetFirst();
                        }
                        line = new Line(points[i], points[Next.Item2]);
                        type = HelperMethods.CheckTurn(line, points[Next1.Item2]);
                    }
                    All.Add(new Tuple<double, int>(angle, i));

                }


            }
            for (int i = 0; i < All.Count; i++)
            {
                outPoints.Add(points[All[i].Item2]);
            }


        }
        public double Check(Point p1, Point p2, Point p3, Point p4)
        {

            double l1 = Math.Atan2(p2.Y - p1.Y, p1.X - p2.X);
            double l2 = Math.Atan2(p4.Y - p3.Y, p3.X - p4.X);
            double angle = (l1 - l2) * (180 / Math.PI);
            return angle;
        }

        public override string ToString()
        {
            return "Convex Hull - Incremental";
        }
    }
}