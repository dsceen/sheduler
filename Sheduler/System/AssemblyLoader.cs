﻿using Microsoft.Extensions.DependencyModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Sheduler.Core.System
{
    public class AssemblyLoader : AssemblyLoadContext
    {
        private readonly string _folderPath;

        public AssemblyLoader(string folderPath)
        {
            _folderPath = folderPath;
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            var deps = DependencyContext.Default;
            var res = deps.CompileLibraries.Where(d => d.Name.Contains(assemblyName.Name)).ToList();
            if (res.Count > 0)
                return Assembly.Load(new AssemblyName(res.First().Name));
            
            var apiApplicationFileInfo = new FileInfo($"{_folderPath}{Path.DirectorySeparatorChar}{assemblyName.Name}.dll");
            if (!File.Exists(apiApplicationFileInfo.FullName)) return Assembly.Load(assemblyName);
            var asl = new AssemblyLoader(apiApplicationFileInfo.DirectoryName);
            return asl.LoadFromAssemblyPath(apiApplicationFileInfo.FullName);
        }
    }
}
