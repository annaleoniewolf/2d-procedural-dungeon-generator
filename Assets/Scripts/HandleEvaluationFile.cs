using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HandleEvaluationFile {
        public static void WriteHeaderToCSV(string filePath, string headerText) {
            using (StreamWriter writer = new StreamWriter(filePath)) {
                // write header
                writer.WriteLine(headerText);
            }
        }

        public static void WriteEvaluationResultsToCSV(string filePath, float density, int roomCount) {
            using (StreamWriter writer = new StreamWriter(filePath, true)) {
                // write results
                writer.WriteLine($"{density},{roomCount}");
            }
        }

        public static void WriteTimeTakenResultsToCSV(string filePath, float timeTaken) {
            using (StreamWriter writer = new StreamWriter(filePath, true)) {
                // write results
                writer.WriteLine($"{timeTaken}");
            }
        }
}
