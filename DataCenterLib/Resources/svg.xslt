<xsl:transform version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="Experiment">
    <svg width="800px" height="1000px" xmlns="http://www.w3.org/2000/svg">
      <g id="experimentheader" transform="translate(0,0)">
        <text x="5" y="40" width="800" style="font-family:arial;font-size:24px;text-anchor:start;">MetaData Overview</text>

        <text x="10" y="70" width="700" style="font-family:arial;font-size:16px;text-anchor:start">
          ELN ID:
        </text>
        <text x="10" y="90" width="700" style="font-family:arial;font-size:16px;text-anchor:start">
          Tracking ID:
        </text>
        <text x="10" y="110" width="700" style="font-family:arial;font-size:16px;text-anchor:start">
          User:
        </text>
        <text x="10" y="130" width="700" style="font-family:arial;font-size:16px;text-anchor:start">
          Project:
        </text>
        <text x="100" y="70" width="700" style="font-family:arial;font-size:16px;text-anchor:start;fill:darkgreen">
          <xsl:value-of select="@UniqueElnId" />
        </text>
        <text x="100" y="90" width="700" style="font-family:arial;font-size:16px;text-anchor:start;fill:darkgreen">
          <xsl:value-of select="@TrackingId" />
        </text>
        <text x="100" y="110" width="700" style="font-family:arial;font-size:16px;text-anchor:start;fill:darkgreen">
          <xsl:value-of select="@User" />
        </text>
        <text x="100" y="130" width="700" style="font-family:arial;font-size:16px;text-anchor:start;fill:darkgreen">
          <xsl:value-of select="@Project" />
        </text>
      </g>
      
      <g id="chemicaltableheader" transform="translate(0,150)">
        <text x="5" y="40" width="800" style="font-family:arial;font-size:24px;text-anchor:start;">Chemical Table</text>
      </g>
      <g id="chemicaltabledata" transform="translate(0,200)" >
        <text x="130" y="25" width="250" style="font-family:arial;text-anchor:middle">Chemical Name</text>
        <text x="330" y="25" width="250" style="font-family:arial;text-anchor:middle">Lot Number</text>
        <text x="550" y="25" width="250" style="font-family:arial;text-anchor:middle">Tracking Id</text>
        <line x1="5" y1="5" x2="795" y2="5" stroke-width="1" stroke="blue"/>
        <line x1="5" y1="35" x2="795" y2="35" stroke-width="1" stroke="blue"/>
        <xsl:for-each select="Chemical">
          <line x1="5" y1="{position()*25 + 35}" x2="795" y2="{position()*25 + 35}" stroke-width="1" stroke="blue"/>

          <text x="10" y="{position()*25 + 30}" width="250" style="font-family:arial;font-size:16px;text-anchor:start;fill:darkgreen">
            <xsl:value-of select="@Name" />
          </text>
          <text x="260" y="{position()*25 + 30}" width="250" style="font-family:arial;font-size:16px;text-anchor:start">
            <xsl:value-of select="@LotNumber" />
          </text>
          <text x="460" y="{position()*25 + 30}" width="250" style="font-family:arial;font-size:16px;text-anchor:start">
            <xsl:value-of select="@TrackingId" />
          </text>
          <line x1="5" y1="5" x2="5" y2="{position()*25 + 35}" stroke-width="1" stroke="blue"/>
          <line x1="255" y1="5" x2="255" y2="{position()*25 + 35}" stroke-width="1" stroke="blue"/>
          <line x1="455" y1="5" x2="455" y2="{position()*25 + 35}" stroke-width="1" stroke="blue"/>
          <line x1="795" y1="5" x2="795" y2="{position()*25 + 35}" stroke-width="1" stroke="blue"/>
        </xsl:for-each>
      </g>

      <xsl:variable name = "ProcessHeaderY" select = "count(Chemical) * 25 + 250"/>

      <xsl:for-each select="Process">
        <xsl:variable name = "ProcessPos" select = "position()"/>
        <g id="processtableheader" transform="translate(0, {$ProcessHeaderY})">
          <text x="5" y="40" width="800"  style="font-family:arial;font-size:24px;text-anchor:start">
            Process (
            <xsl:value-of select="@ProcessType" />
            )
          </text>

          <xsl:for-each select="Stage">
            <xsl:variable name="stageName" select="@StageType"/>
            <xsl:for-each select="Phase">
              <xsl:variable name = "PhasePos" select = "count(preceding::Phase) * 125 + 70"/>
              <text x="20" y="{$PhasePos}" width="800"  style="font-family:arial;font-size:16px;text-anchor:left" >
                Stage (<xsl:value-of select="$stageName" />)&#160; Phase &#160; <xsl:value-of select="position()" />
              </text>
              <xsl:for-each select="OperationSequence">
                <xsl:variable name = "LanePos" select = "(position()-1) * 200 + 40"/>
                <xsl:for-each select="*">
                  <xsl:variable name = "OperPos" select = "$PhasePos + (position() * 25)"/>
                  <text x="{$LanePos}" y="{$OperPos}" width="800"  style="font-family:arial;font-size:16px;text-anchor:left" >
                    (<xsl:value-of select="name(.)"/>)
                  </text>
                </xsl:for-each>
              </xsl:for-each>
            </xsl:for-each>
          </xsl:for-each>
        </g>
      </xsl:for-each>
    </svg>
  </xsl:template>
</xsl:transform>