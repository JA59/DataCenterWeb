using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DataCenterCommon.Svg
{
    public class SvgGenerator
    {
        private XDocument m_doc;
        private StringBuilder m_output;
        private int m_swimLaneTop;
        private int m_boxTop;

        private int m_swimLaneBottom;
        private int m_swimLaneCount;
        private string m_name;
        private string m_user;
        private string m_project;
        private string m_trackingId;



        public SvgGenerator(XDocument plannedExperiment)
        {
            m_doc = plannedExperiment;
            m_output = new StringBuilder();
            m_name = null;
            m_user = null;
            m_project = null;
            m_trackingId = null;
        }

        public SvgGenerator(string name, string user, string project, string trackingId)
        {
            m_doc = XDocument.Parse(Properties.Resources.SAMPLE);
            m_output = new StringBuilder();
            m_name = name;
            m_user = user;
            m_project = project;
            m_trackingId = trackingId;

        }

        public string GetSvg()
        {
            StringBuilder output = new StringBuilder();
            m_swimLaneCount = 4;
            int bottom = AddExperiment(m_doc.FirstNode);
            output.AppendLine(String.Format("<svg width=\"820px\" height=\"{0}px\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" >", bottom).ToString());
            output.AppendLine(AppendFrame());
            output.AppendLine(AppendSwimLanes());

            output.AppendLine(m_output.ToString());
            output.AppendLine("</svg>");
            return output.ToString();
        }

        public int AddExperiment(XNode experiment)
        {
            int y = AddExperimentMetaData(experiment as XElement);
            y = AddChemicalTable(experiment as XElement, y+60);
            y = AddProcess(experiment as XElement, y + 60);
            return y+30;
        }

        public int AddExperimentMetaData(XElement experiment)
        {
            int y = 50;
            m_output.AppendLine("  <g id=\"experimentheader\" transform=\"translate(10, 0)\">");

            var name = "(not specified)";
            if (!String.IsNullOrEmpty(m_name))
            {
                name = m_name;
            }
            else if (experiment.Attribute("Name") != null)
            {
                var tmp = experiment.Attribute("Name").Value;
                if (!String.IsNullOrEmpty(tmp))
                {
                    name = tmp;
                }
            }

            AppendText(0, y, 16, "ELN ID:", "");
            var elnId = m_name == null ? experiment.Attribute("UniqueElnId").Value : m_name;
            AppendText(150, y, 16, elnId, "fill:darkgreen"); y = y + 20;

            AppendText(0, y, 16, "Name:", "");
            AppendText(150, y, 16, name, "fill:darkgreen"); y = y + 20;

            AppendText(0, y, 16, "Tracking ID:", "");
            var trackingId = m_trackingId == null ? experiment.Attribute("TrackingId").Value : m_trackingId;
            AppendText(150, y, 16, trackingId, "fill:darkgreen"); y = y + 20;

            AppendText(0, y, 16, "User:", "");
            var user = m_user == null ? experiment.Attribute("User").Value : m_user;
            AppendText(150, y, 16, user, "fill:darkgreen"); y = y + 20;

            AppendText(0, y, 16, "Project:", "");
            var project = m_project == null ? experiment.Attribute("Project").Value : m_project;
            AppendText(150, y, 16, project, "fill:darkgreen"); y = y + 20;

            AppendText(0, y, 16, "Schema Version:", "");
            AppendText(150, y, 16, experiment.Attribute("SchemaVersion").Value, "fill:darkgreen"); y = y + 20;
            m_output.AppendLine("  </g>");
            return y-20;
        }

        private void AppendHeaderText(int x, int y, string text, string option)
        {
            m_output.AppendLine (String.Format("    <text x=\"{0}\" y=\"{1}\" width=\"800\" style=\"font-family:arial; font-size:{2}px; text-anchor:start;{4} \">{3}</text>", x, y, 36, text, option));
        }

        private void AppendText(int x, int y, int size, string text, string option)
        {
            m_output.AppendLine(String.Format("    <text x=\"{0}\" y=\"{1}\" width=\"800\" style=\"font-family:Segoe UI; font-size:{2}px; text-anchor:start;{4} \">{3}</text>", x, y, size, text, option));
        }

        private void AppendCenterText(int x, int y, int size, int width, string text, string option)
        {
            m_output.AppendLine(String.Format("    <text x=\"{0}\" y=\"{1}\" width=\"{5}\" style=\"font-family:Segoe UI; font-size:{2}px; text-anchor:middle;{4} \">{3}</text>", x, y, size, text, option, width));
        }

        private void AppendLine(int x1, int y1, int x2, int y2, string color)
        {
            m_output.AppendLine(String.Format("    <line x1=\"{0}\" y1=\"{1}\" x2=\"{2}\" y2=\"{3}\" stroke-width=\"1\" stroke=\"{4}\"/>", x1, y1, x2, y2, color));
        }

        private int AppendOperation(int x, int y, string name)
        {
            m_output.AppendLine(String.Format("    <rect x=\"{0}\" y=\"{1}\" width=\"170\" height=\"60\" rx=\"5\" style=\"fill: white; stroke: gray; stroke-width:1\" />", x + 10, y));
            AppendCenterText(x + 95, y + 22, 12, 158, name, "");

            var array = IconFactory.GetIcon(name);
            if (array != null)
            { 
                var image = (String.Format("<image id=\"IMG{0}\" x=\"{1}\" y=\"{2}\" width=\"32\" height=\"32\" xlink:href=\"data:image/png;base64,{3}\" />",
                    Guid.NewGuid().ToString(), x + 13, y + 3,
                   Convert.ToBase64String(array)
                ));
                m_output.AppendLine(image);
            }



            return 65;
        }

        private int AppendPhase(int y, string stage, int stageNumber, int phaseNumber, string phaseName)
        {
            m_output.AppendLine(String.Format("    <rect x=\"2\" y=\"{0}\" width=\"796\" height=\"35\" rx=\"0\" style=\"fill: LightBlue; stroke: black; stroke-width:0\" />", y));
            AppendText(20, y + 25, 20, String.Format("{0}. {1}", stageNumber, stage), "");

            m_output.AppendLine(String.Format("    <rect x=\"20\" y=\"{0}\" width=\"760\" height=\"35\" rx=\"3\" style=\"fill: white; stroke: gray; stroke-width:1\" />", y+50));
            AppendText(30, y + 70, 16, String.Format("Phase {0}: {1}", phaseNumber, phaseName), "");

            return 100;
            //AppendText(210, y + 25, 16, String.Format("Phase {0}", phaseNumber), "");
        }

        public int AddChemicalTable(XElement experiment, int startingY)
        {
            IEnumerable<XElement> chemicalElements = experiment.Elements("Chemical");
            if (chemicalElements.Count() == 0)
            {
                return startingY - 60;
            }

            int y = 0;
            m_output.AppendLine(String.Format("  <g id=\"chemicaltable\" transform=\"translate(10, {0})\">", startingY));
            AppendHeaderText(0, y, "Chemistry", ""); y = y + 35;

            m_output.AppendLine(String.Format("    <rect x=\"1\" y=\"15\" width=\"798\" height=\"30\" rx=\"0\" style=\"fill: wheat; stroke: black; stroke-width:2\" />"));



            AppendText(10, 35, 16, "Chemical Name", "");
            AppendText(260, 35, 16, "Lot Number", "");
            AppendText(460, 35, 16, "Tracking ID", "");
            y = y + 30;


            foreach ( var chemical in chemicalElements)
            {
                AppendText(10, y, 16, chemical.Attribute("Name").Value, "fill:saddlebrown");
                AppendText(260, y, 16, chemical.Attribute("LotNumber").Value, "fill:saddlebrown");
                AppendText(460, y, 16, chemical.Attribute("TrackingId").Value, "fill:saddlebrown");
                y = y + 25;
            }

            y = y - 20;
            m_output.AppendLine(String.Format("    <rect x=\"1\" y=\"15\" width=\"798\" height=\"{0}\" style=\"fill: none; stroke: black; stroke-width:2\" />", y));



            m_output.AppendLine("  </g>");
            return startingY + y;
        }

        public int AddProcess(XElement experiment, int startingY)
        {
            int y = 0;
            m_boxTop = startingY + 30;
            m_swimLaneTop = startingY + 80;

            IEnumerable<XElement> processElements = experiment.Elements("Process");
            foreach (var process in processElements)
            {
                m_output.AppendLine(String.Format("  <g id=\"processarea\" transform=\"translate(10, {0})\">", startingY));
                AppendHeaderText(0, y+15, "Procedure", ""); y = y + 30;
                m_output.AppendLine(String.Format("    <rect x=\"2\" y=\"{0}\" width=\"796\" height=\"50\" rx=\"0\" style=\"fill: #B0D0FF; stroke: black; stroke-width:0\" />", y));
                AppendText(10, y + 35, 22, process.Attribute("ProcessType").Value, ""); y = y + 50;

                IEnumerable<XElement> stageElements = process.Elements("Stage");
                int stageIndex = 0;
                foreach (var stage in stageElements)
                {
                    stageIndex++;
                    int phaseNumber = 0;
                    IEnumerable<XElement> phaseElements = stage.Elements("Phase");
                    foreach (var phase in phaseElements)
                    {
                        phaseNumber++;
                        var yTop = y;
                        var pName = phase.Attribute("Comment")?.Value;
                        // draw the stage / phase header
                        y = y + AppendPhase(y, stage.Attribute("StageType").Value, stageIndex, phaseNumber, String.IsNullOrEmpty(pName) ? "" : pName);

                        int maxY = y;
                        int startY = y;
                        int x = 10;
                        // Now draw the sequences of operations
                        IEnumerable<XElement> sequenceElements = phase.Elements("OperationSequence");
                        if (sequenceElements.Count() > m_swimLaneCount)
                            m_swimLaneCount = sequenceElements.Count();
                        foreach (var sequence in sequenceElements)
                        {
                            IEnumerable<XElement> operationElements = sequence.Elements();
                            foreach (var operation in operationElements)
                            {
                                var name = operation.Name.LocalName;
                                name = name.Substring(0, name.Length - 9); // strip off "Operation"
                                y = y + AppendOperation(x, y, name);
                                if (y > maxY) maxY = y;
                            }
                            x = x + 190;
                            y = startY;
                        }

                        y = maxY + 30;

                    }

                }




                m_output.AppendLine("  </g>");
                break; // just do one
            }

            m_swimLaneBottom = startingY + y;

            return m_swimLaneBottom;

        }

        private string AppendSwimLanes()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(String.Format("  <g id=\"swimlanes\" transform=\"translate(10, 0)\">"));

            for (int i=0; i< m_swimLaneCount; i++)
            {
                if (i%2 == 0)
                {
                    // blue lane
                    int w = 800 - ((190 * i) + 10);
                    if (w > 190) w = 190;
                    result.AppendLine(String.Format("    <rect x=\"{0}\" y=\"{1}\" width=\"{2}\" height=\"{3}\" rx=\"0\" style=\"fill: #EDF7Fc; stroke: black; stroke-width:0\" />", (190*i) + 10, m_swimLaneTop, w, m_swimLaneBottom - m_swimLaneTop));

                }
                else
                {
                    // white lane (do nothing)
                }


            }
            result.AppendLine(String.Format("    <rect x=\"1\" y=\"{0}\" width=\"798\" height=\"{1}\" rx=\"0\" style=\"fill: none; stroke: black; stroke-width:2\" />", m_boxTop, m_swimLaneBottom - m_boxTop));

            result.AppendLine("  </g>");
            return result.ToString();

        }


        private string AppendFrame()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(String.Format("  <g id=\"frame\" transform=\"translate(0, 0)\">"));

            result.AppendLine(String.Format("    <rect x=\"0\" y=\"0\" width=\"{0}\" height=\"{1}\" rx=\"0\" style=\"fill: #FFFFFF; stroke: black; stroke-width:3\" />", 820, m_swimLaneBottom + 10));
            result.AppendLine("  </g>");
            return result.ToString();

        }

    }
}
