// For Directory.GetFiles and Directory.GetDirectories 
// For File.Exists, Directory.Exists 
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class RecursiveFileProcessor 
{
	// Process all files in the directory passed in, recurse on any directories  
	// that are found, and process the files they contain. 
	public static List<string> ProcessDirectory(string targetDirectory) 
	{
		List<string> list = new List<string>();

		// Process the list of files found in the directory. 
		string [] fileEntries = Directory.GetFiles(targetDirectory);
		foreach(string fileName in fileEntries)
			list.Add(fileName);
		
		// Recurse into subdirectories of this directory. 
		string [] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
		foreach(string subdirectory in subdirectoryEntries)
			ProcessDirectory(subdirectory);

		return list;
	}

	public static int countFilesInDirectory(string dir)
	{
		return Directory.GetFiles(dir).Length;
	}
}