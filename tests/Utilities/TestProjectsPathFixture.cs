﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

using System;
using System.IO;
using System.Threading;

namespace Microsoft.CodeAnalysis.Tools.Tests.Utilities
{
    /// <summary>
    /// This test fixture sets the <see cref="Environment.CurrentDirectory" /> to the dotnet-format test projects folder path.
    /// </summary>
    public sealed class TestProjectsPathFixture : IDisposable
    {
        private static int s_registered;
        private static string s_currentDirectory;

        public string SetCurrentDirectory()
        {
            if (Interlocked.Increment(ref s_registered) == 1)
            {
                s_currentDirectory = Environment.CurrentDirectory;
                var solutionPath = Directory.GetParent(s_currentDirectory).Parent.Parent.Parent.Parent.FullName;
                Environment.CurrentDirectory = Path.Combine(solutionPath, "tests", "projects");
            }

            return Environment.CurrentDirectory;
        }

        public void Dispose()
        {
            if (Interlocked.Decrement(ref s_registered) == 0)
            {
                Environment.CurrentDirectory = s_currentDirectory;
                s_currentDirectory = null;
            }
        }
    }
}
