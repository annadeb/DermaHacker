using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DermaHacker.Models.Database
{
    public class Report
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string NameAndSurname { get; set; }
        public DateTime Date { get; set; }
        public string StandardImagePath { get; set; }
        public string ThermoImagePath { get; set; }
        //public Size Size { get; set; }
        //public WoundBase WoundBase { get; set; }
        //public Temperature Temperature { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Surface { get; set; }
        public double WoundBaseTemperature { get; set; }
        public double SurroundingsTemperature { get; set; }
        public double GranulationTissuePercentage { get; set; }
        public double SludgePercentage { get; set; }
        public double NecrosisPercentage { get; set; }
    }
}
