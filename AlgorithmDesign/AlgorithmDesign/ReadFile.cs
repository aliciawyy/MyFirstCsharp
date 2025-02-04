﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AlgorithmDesign
{
    public class ReadFile
    {
        static readonly string datapath = Path.Combine(Directory.GetCurrentDirectory(), "../../../../data");

        public static string GetInputFileName()
        {
            var files = Directory.EnumerateFiles (datapath, "*.txt").Select (f => Path.GetFileName (f));
            Console.WriteLine ("Data files in the data directory :");
            foreach (string fi in files) {
                Console.Write("{0} ", fi);
            }
            Console.WriteLine ("\nEnter the file name :");
            string filename = Console.ReadLine();
            string fullname = Path.Combine(datapath, filename);

            return fullname;
        }

        public static List<int> ReadIntFile(string filename)
        {
            FileInfo fi = new FileInfo (filename);
            if (!fi.Exists) {
                throw new FileNotFoundException ();
            }
                
            List<int> v = new List<int> (); // New int list

            using (FileStream fs = File.OpenRead (filename))
            using (TextReader reader = new StreamReader (fs)) 
            {
                while (reader.Peek () > -1) {
                    string s = reader.ReadLine ();
                    v.Add (Convert.ToInt32 (s));
                }
            }

            return v;
        }
        
        /// <summary>
        /// Reads all edges of a _directed_ graph. An example of input file is
        /// 1 2
        /// 2 425
        /// 2 33
        /// ...
        /// The index starts from 1 and each row contains two terms where the first is the tail and the second
        /// is the head
        /// </summary>
        /// <param name="filename">Filename.</param>
        /// <param name="tail">List of the tails</param>
        /// <param name="head">List of the head</param>
        public static void ReadAllEdges(string filename, List<int> tail, List<int> head)
        {
            FileInfo fi = new FileInfo (filename);
            if (!fi.Exists) {
                throw new FileNotFoundException ();
            }

            tail.Clear ();
            head.Clear ();

            using (FileStream fs = File.OpenRead (filename))
            using (TextReader reader = new StreamReader (fs)) 
            {
                while (reader.Peek () > -1) {
                    string[] tokens = reader.ReadLine().Split();
                    tail.Add (int.Parse (tokens [0]) - 1);
                    head.Add (int.Parse (tokens [1]) - 1);
                }
            }
                
            return;
        }

        /// <summary>
        /// Converts all edges representation to adjacent list representation of a graph.
        /// </summary>
        /// <param name="tail">Tail.</param>
        /// <param name="head">Head.</param>
        /// <param name="nvert">number of vertices</param>
        /// <param name="dgraph">output adjacent list representation of the graph</param>
        public static void ConvertAllEdgesToAdjacentList(List<int> tail, List<int> head, int nvert, List<List<int> > dgraph)
        {
            if (dgraph != null) {
                dgraph.Clear ();
            } else {
                dgraph = new List<List<int>> ();
            }

            for (int i = 0; i < nvert; ++i) {
                List<int> u = new List<int> ();
                dgraph.Add (u);
            }

            for (int i = 0; i < tail.Count; ++i) {
                int ind = tail [i];
                dgraph[ind].Add (head [i]);
            }
        }
    }
}

