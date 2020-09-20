// <copyright file="Program.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;

namespace Covid.Migration
{
    /// <summary>
    /// Migrations.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        public static void Main(/* string[] args */)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine();
            }

            ConsoleColor save = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(@"          This is the migration app");
            Console.ForegroundColor = save;

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine();
            }
        }
    }
}