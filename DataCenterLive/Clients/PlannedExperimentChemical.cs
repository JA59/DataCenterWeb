using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DataCenterLive.Clients
{
    /// <summary>
    /// Defines a chemical in a planned experiment
    /// Includes just the chemical name
    /// </summary>
    [DataContract(Namespace = ServicesHelper.TypeNameSpace)]
    [Serializable]
    public class PlannedExperimentChemical
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PlannedExperimentChemical()
        {
            ChemicalName = String.Empty;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="chemicalName"></param>
        /// <param name="uniqueId"></param>
        /// <param name="lotNumber"></param>
        /// <param name="mass"></param>
        /// <param name="volume"></param>
        public PlannedExperimentChemical(string chemicalName, Guid uniqueId, string lotNumber, string mass, string volume)
        {
            UniqueID = uniqueId;
            ChemicalName = chemicalName;
            LotNumber = lotNumber;
            Mass = mass;
            Volume = volume;
        }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        [DataMember]
        public Guid UniqueID { get; set; }

        /// <summary>
        /// Chemical name.
        /// </summary>
        [DataMember]
        public string ChemicalName { get; set; }

        /// <summary>
        /// Lot number.
        /// </summary>
        [DataMember]
        public string LotNumber { get; set; }

        /// <summary>
        /// Mass.
        /// </summary>
        [DataMember]
        public string Mass { get; set; }

        /// <summary>
        /// Volume.
        /// </summary>
        [DataMember]
        public string Volume { get; set; }
    }
}
