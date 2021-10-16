using System;
using System.Collections.Generic;
using System.Linq;

namespace Aeroplane
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] cabinSpec = { new int[] { 3, 2 }, new int[] { 4, 3 }, new int[] { 2, 3 }, new int[] { 3, 4 } };
            List<int[][]> tolu = OccupyPlane(cabinSpec, 30);
            for (int i = 0; i < 4; i++)
            {
                List<int[][]> arr = new List<int[][]>();
                foreach (var item in tolu)
                {
                    try
                    {
                        int[] ewee = item[i];
                        foreach (var c in ewee)
                            Console.Write(c + " ");

                    }
                    catch (Exception)
                    {
                        Console.Write(" ");
                        continue;
                    }
                }
                Console.WriteLine();

            }
        }
        public static List<int[][]> OccupyPlane(int[][] cabinSpec, int passangerCount)
        {
            List<int[][]> cabin = new List<int[][]>();
            Dictionary<string, List<Seat>> buffer = new Dictionary<string, List<Seat>>()
        {
            {"window", new List<Seat>() },
            {"aisle", new List<Seat>() },
            {"middle", new List<Seat>() }
        };
            List<Seat> final = new List<Seat>();
            int max = 0;
            foreach (int[] x in cabinSpec)
            {
                max = max < x[1] ? x[1] : max;
                cabin.Add(MatCreate(x[1], x[0]));
            }

            for (int i = 0; i < max; i++)
            {
                for (int section_index = 0; section_index < cabin.Count; section_index++)
                {
                    try
                    {
                        int[] row = cabin[section_index][i];
                        for (int seat_index = 0; seat_index < row.Length; seat_index++)
                        {

                            if ((section_index == 0 && seat_index == 0) || (section_index == cabinSpec.Length - 1 && seat_index == row.Length - 1))
                                buffer["window"].Add(new Seat(new int[] { section_index, i, seat_index }, seat_index, "window"));
                            else if (seat_index == 0 || seat_index == row.Length - 1)
                                buffer["aisle"].Add(new Seat(new int[] { section_index, i, seat_index }, seat_index, "aisle"));
                            else
                                buffer["middle"].Add(new Seat(new int[] { section_index, i, seat_index }, seat_index, "middle"));
                        }
                    }
                    catch (Exception)
                    {
                        continue;

                    }
                }
            }
            final = (buffer["aisle"].Concat(buffer["window"]).Concat(buffer["middle"])).ToList();
            int count = 1;
            foreach (Seat seat in final)
            {
                int[] i = seat.Position;
                cabin[i[0]][i[1]][i[2]] = count;
                count++;
                if (count > passangerCount)
                    break;
            }
            return cabin;
        }
        public static int[][] MatCreate(int rows, int cols)
        {
            int[][] result = new int[rows][];
            for (int i = 0; i < rows; ++i)
                result[i] = new int[cols];
            return result;
        }
    }
    public class Seat
    {
        private int[] position;
        private int index;
        private string seatType;

        public Seat(int[] pos, int ind, string seat)
        {
            position = pos;
            index = ind;
            seatType = seat;
        }
        public int[] Position
        {
            get { return position; }
        }
        public int Index
        {
            get { return index; }
        }
        public string SeaT
        {
            get { return seatType; }
        }

    }
}
