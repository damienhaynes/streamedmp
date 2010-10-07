namespace StreamedMPEditor
{
  public partial class formStreamedMpEditor
  {

    public int baseXPosAdded;
    public int baseYPosAdded;
    public int baseXPosWatched;
    public int baseYPosWatched;

    #region Main

    void generateMostRecentOverlay(chosenMenuStyle menuStyle, isOverlayType isOverlay, int xPosAdded, int yPosAdded, int xPosWatched, int yPosWatched)
    {
      baseXPosAdded = xPosAdded;
      baseYPosAdded = yPosAdded;
      baseXPosWatched = xPosWatched;
      baseYPosWatched = yPosWatched;

      if (isOverlay == isOverlayType.TVSeries)
      {
        doTVSeries(tvSeriesRecentStyle);
      }
      if (isOverlay == isOverlayType.MovPics)
      {
        doMovingPictures(movPicsRecentStyle);
      }
    }
    //
    // TVSeries - Summary or Full, vertical or horizonal
    //
    void doTVSeries(tvSeriesRecentType reqType)
    {
      if (menuStyle == chosenMenuStyle.verticalStyle)
      {
        if (reqType == tvSeriesRecentType.summary)
        {
          verticalTVSeriesSummary();
        }
        else
        {
          verticalTVSeriesFull();
        }
      }
      else
      {
        if (reqType == tvSeriesRecentType.summary)
        {
          baseXPosWatched = 5;
          baseYPosWatched = 50;
          horizontalTVSeriesSummary();
        }
        else
        {
          horizonalTVSeriesFull();
        }
      }
    }
    //
    // MovingPictures - Summary or Full, vertical or horizonal
    //
    void doMovingPictures(movPicsRecentType reqType)
    {
      if (menuStyle == chosenMenuStyle.verticalStyle)
      {
        if (reqType == movPicsRecentType.summary)
        {
          verticalMovingPicturesSummary();
        }
        else
        {
          verticalMovingPicturesFull();
        }
      }
      else
      {
        if (reqType == movPicsRecentType.summary)
        {
          horizontalMovingPicturesSummary();
        }
        else
        {
          horizonalMovingPicturesFull();
        }
      }
    }


    #endregion

    #region TVSeries Vertical Full

    void verticalTVSeriesFull()
    {
      xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>\n" +
            "<window>\n" +
              "<controls>\n" +
                "<control>\n" +
                  "<description>GROUP: RecentlyAdded Series</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#StreamedMP.recentlyAdded.series1.thumb,#)</visible>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "<control>\n" +
                    "<description>Series 1 BG</description>\n" +
                    "<posX>890</posX>\n" +
                    "<posY>70</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>385</width>\n" +
                    "<height>180</height>\n" +
                    "<texture>recentfullbg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 thumb</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>900</posX>\n" +
                    "<posY>79</posY>\n" +
                    "<width>112</width>\n" +
                    "<height>165</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>#StreamedMP.recentlyAdded.series1.thumb</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 Rounded Cover</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>892</posX>\n" +
                    "<posY>71</posY>\n" +
                    "<height>181</height>\n" +
                    "<width>129</width>\n" +
                    "<texture>round.poster.frame.noreflection.png</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 name</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>98</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.series1.title</label>\n" +
                    "<font>mediastream12tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 title</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>151</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.series1.episodetitle</label>\n" +
                    "<font>mediastream12</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 episode</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>206</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#TVSeries.Translation.Season.Label: #StreamedMP.recentlyAdded.series1.season #TVSeries.Translation.Episode.Label: #StreamedMP.recentlyAdded.series1.episodenumber</label>\n" +
                    "<font>mediastream10</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                "</control>\n" +
                "<control>\n" +
                  "<description>GROUP: RecentlyAdded Series</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#StreamedMP.recentlyAdded.series2.thumb,#)</visible>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "<control>\n" +
                    "<description>Series 2 BG</description>\n" +
                    "<posX>890</posX>\n" +
                    "<posY>270</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>385</width>\n" +
                    "<height>180</height>\n" +
                    "<texture>recentfullbg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 thumb</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>902</posX>\n" +
                    "<posY>279</posY>\n" +
                    "<width>112</width>\n" +
                    "<height>165</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>#StreamedMP.recentlyAdded.series2.thumb</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 Rounded Cover</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>892</posX>\n" +
                    "<posY>270</posY>\n" +
                    "<height>181</height>\n" +
                    "<width>129</width>\n" +
                    "<texture>round.poster.frame.noreflection.png</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 name</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>299</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.series2.title</label>\n" +
                    "<font>mediastream12tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 title</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>352</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.series2.episodetitle</label>\n" +
                    "<font>mediastream12</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 episode</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>405</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#TVSeries.Translation.Season.Label: #StreamedMP.recentlyAdded.series2.season #TVSeries.Translation.Episode.Label: #StreamedMP.recentlyAdded.series2.episodenumber</label>\n" +
                    "<font>mediastream10</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                "</control>\n" +
                "<control>\n" +
                  "<description>GROUP: RecentlyAdded Series</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#StreamedMP.recentlyAdded.series3.thumb,#)</visible>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "<control>\n" +
                    "<description>Series 3 BG</description>\n" +
                    "<posX>890</posX>\n" +
                    "<posY>470</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>385</width>\n" +
                    "<height>180</height>\n" +
                    "<texture>recentfullbg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 thumb</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>901</posX>\n" +
                    "<posY>478</posY>\n" +
                    "<width>112</width>\n" +
                    "<height>165</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>#StreamedMP.recentlyAdded.series3.thumb</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 Rounded Cover</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>892</posX>\n" +
                    "<posY>470</posY>\n" +
                    "<height>181</height>\n" +
                    "<width>129</width>\n" +
                    "<texture>round.poster.frame.noreflection.png</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 name</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>498</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.series3.title</label>\n" +
                    "<font>mediastream12tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 title</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>551</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.series3.episodetitle</label>\n" +
                    "<font>mediastream12</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 episode</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>604</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#TVSeries.Translation.Season.Label: #StreamedMP.recentlyAdded.series3.season #TVSeries.Translation.Episode.Label: #StreamedMP.recentlyAdded.series3.episodenumber</label>\n" +
                    "<font>mediastream10</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                "</control>\n" +
              "</controls>\n" +
            "</window>";

      writeXMLFile("basichome.recentlyadded.tvseries.VFull.xml");

    }

    #endregion

    #region TVSeries Vertical Summary

    void verticalTVSeriesSummary()
    {
      if (mrTVSeriesSummStyle == mostRecentTVSeriesSummaryStyle.poster)
      {
        buildTVSeriesSummaryFile(475, mostRecentTVSeriesSummaryStyle.poster);
        writeXMLFile("basichome.recentlyadded.tvseries.VSum.xml");
      }
      if (mrTVSeriesSummStyle == mostRecentTVSeriesSummaryStyle.fanart)
      {
        buildTVSeriesSummaryFile(475, mostRecentTVSeriesSummaryStyle.fanart);
        if (cbTVSeriesRecentWatched.Checked)
          mostRecentTVSeriesWatched();

        writeXMLFile("basichome.recentlyadded.tvseries.VSum2.xml");
      }
    }

    #endregion

    #region TVSeries Horizontal Full

    void horizonalTVSeriesFull()
    {
      //
      // Work out the Ypos
      //
      int overlayYpos = 0;
      int maxYpos = 536;
      int overlayOffset = 184;
      int hdOffset = 0;

      int menuTop = int.Parse(txtMenuPos.Text) + basicHomeValues.offsetMymenu + 54;
      int menuBot = int.Parse(txtMenuPos.Text) + basicHomeValues.offsetSubmenu;

      if (screenres == screenResolutionType.res1920x1080)
        hdOffset = 0;
      else
        hdOffset = 0;

      if (enableTwitter.Checked)
        menuTop = int.Parse(txtMenuPos.Text) - basicHomeValues.offsetTwitter + 30;

      if (enableRssfeed.Checked && infoserviceOptions.Enabled)
        menuBot += (basicHomeValues.subMenuHeight - 30);

      if (menuBot > maxYpos)
      {
        overlayYpos = menuTop - overlayOffset;
        if (screenres == screenResolutionType.res1920x1080)
          hdOffset -= 54;
      }
      else
        overlayYpos = 536;

      // Build the file
      xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>" +
            "<window>" +
              "<controls>" +
                "<control>" +
                  "<description>GROUP: RecentlyAdded Series</description>" +
                  "<type>group</type>" +
                  "<dimColor>0xffffffff</dimColor>" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#StreamedMP.recentlyAdded.series1.thumb,#)</visible>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,300" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                  "<control>" +
                    "<description>Series 1 BG</description>" +
                    "<posX>28</posX>" +
                    "<posY>" + (overlayYpos + 2).ToString() + "</posY>" +
                    "<type>image</type>" +
                    "<id>6777</id>" +
                    "<width>385</width>" +
                    "<height>180</height>" +
                    "<texture>recentfullbg.png</texture>" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 1 thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>36</posX>" +
                    "<posY>" + (overlayYpos + 10).ToString() + "</posY>" +
                    "<height>163</height>" +
                    "<width>111</width>" +
                    "<keepaspectratio>true</keepaspectratio>" +
                    "<texture>#StreamedMP.recentlyAdded.series1.thumb</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 1 Rounded Cover</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>26</posX>" +
                    "<posY>" + (overlayYpos + 2).ToString() + "</posY>" +
                    "<height>181</height>" +
                    "<width>129</width>" +
                    "<texture>round.poster.frame.noreflection.png</texture>" +
                  "</control>      " +
                "<control>" +
                    "<description>Series 1 name</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>152</posX>" +
                    "<posY>" + (overlayYpos + 29).ToString() + "</posY>" +
                    "<width>258</width>" +
                    "<label>#StreamedMP.recentlyAdded.series1.title</label>" +
                    "<font>mediastream12tc</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 1 title</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>152</posX>" +
                    "<posY>" + (overlayYpos + 82).ToString() + "</posY>" +
                    "<width>255</width>" +
                    "<label>#StreamedMP.recentlyAdded.series1.episodetitle</label>" +
                    "<font>mediastream12</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 1 episode</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>152</posX>" +
                    "<posY>" + (overlayYpos + 139).ToString() + "</posY>" +
                    "<width>255</width>" +
                    "<label>#TVSeries.Translation.Season.Label: #StreamedMP.recentlyAdded.series1.season #TVSeries.Translation.Episode.Label: #StreamedMP.recentlyAdded.series1.episodenumber</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                "</control>" +
                "<control>" +
                  "<description>GROUP: RecentlyAdded Series</description>" +
                  "<type>group</type>" +
                  "<dimColor>0xffffffff</dimColor>" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#StreamedMP.recentlyAdded.series2.thumb,#)</visible>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,300" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                  "<control>" +
                    "<description>Series 2 BG</description>" +
                    "<posX>442</posX>" +
                    "<posY>" + (overlayYpos + 2).ToString() + "</posY>" +
                    "<type>image</type>" +
                    "<id>6777</id>" +
                    "<width>385</width>" +
                    "<height>180</height>" +
                    "<texture>recentfullbg.png</texture>" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 2 thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>452</posX>" +
                    "<posY>" + (overlayYpos + 10).ToString() + "</posY>" +
                    "<height>163</height>" +
                    "<width>111</width>" +
                    "<keepaspectratio>true</keepaspectratio>" +
                    "<texture>#StreamedMP.recentlyAdded.series2.thumb</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 2 Rounded Cover</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>440</posX>" +
                    "<posY>" + (overlayYpos + 1).ToString() + "</posY>" +
                    "<height>181</height>" +
                    "<width>127</width>" +
                    "<texture>round.poster.frame.noreflection.png</texture>" +
                  "</control>      " +
                  "<control>" +
                    "<description>Series 2 name</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>568</posX>" +
                    "<posY>" + (overlayYpos + 29).ToString() + "</posY>" +
                    "<width>258</width>" +
                    "<label>#StreamedMP.recentlyAdded.series2.title</label>" +
                    "<font>mediastream12tc</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 2 title</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>565</posX>" +
                    "<posY>" + (overlayYpos + 82).ToString() + "</posY>" +
                    "<width>255</width>" +
                    "<label>#StreamedMP.recentlyAdded.series2.episodetitle</label>" +
                    "<font>mediastream12</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 2 episode</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>565</posX>" +
                    "<posY>" + (overlayYpos + 139).ToString() + "</posY>" +
                    "<width>258</width>" +
                    "<label>#TVSeries.Translation.Season.Label: #StreamedMP.recentlyAdded.series2.season #TVSeries.Translation.Episode.Label: #StreamedMP.recentlyAdded.series2.episodenumber</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                "</control>" +
                "<control>" +
                  "<description>GROUP: RecentlyAdded Series</description>" +
                  "<type>group</type>" +
                  "<dimColor>0xffffffff</dimColor>" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#StreamedMP.recentlyAdded.series3.thumb,#)</visible>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,300" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                  "<control>" +
                    "<description>Series 3 BG</description>" +
                    "<posX>855</posX>" +
                    "<posY>" + (overlayYpos + 2).ToString() + "</posY>" +
                    "<type>image</type>" +
                    "<id>6777</id>" +
                    "<width>385</width>" +
                    "<height>180</height>" +
                    "<texture>recentfullbg.png</texture>" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 3 thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>866</posX>" +
                    "<posY>" + (overlayYpos + 10).ToString() + "</posY>" +
                    "<height>163</height>" +
                    "<width>111</width>" +
                    "<keepaspectratio>true</keepaspectratio>" +
                    "<texture>#StreamedMP.recentlyAdded.series3.thumb</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 3 Rounded Cover</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>858</posX>" +
                    "<posY>" + (overlayYpos + 2).ToString() + "</posY>" +
                    "<height>181</height>" +
                    "<width>127</width>" +
                    "<texture>round.poster.frame.noreflection.png</texture>" +
                  "</control>      " +
                  "<control>" +
                    "<description>Series 3 name</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>981</posX>" +
                    "<posY>" + (overlayYpos + 29).ToString() + "</posY>" +
                    "<width>255</width>" +
                    "<label>#StreamedMP.recentlyAdded.series3.title</label>" +
                    "<font>mediastream12tc</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 3 title</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>981</posX>" +
                    "<posY>" + (overlayYpos + 82).ToString() + "</posY>" +
                    "<width>255</width>" +
                    "<label>#StreamedMP.recentlyAdded.series3.episodetitle</label>" +
                    "<font>mediastream12</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 3 episode</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>981</posX>" +
                    "<posY>" + (overlayYpos + 139).ToString() + "</posY>" +
                    "<width>255</width>" +
                    "<label>#TVSeries.Translation.Season.Label: #StreamedMP.recentlyAdded.series3.season #TVSeries.Translation.Episode.Label: #StreamedMP.recentlyAdded.series3.episodenumber</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                "</control>" +
                "</controls>" +
            "</window>";

      writeXMLFile("basichome.recentlyadded.tvseries.HFull.xml");

    }

    #endregion

    #region TVSeries Horizontal Summary

    void horizontalTVSeriesSummary()
    {
      //
      // Work out the Ypos
      //
      int overlayYpos = 0;
      int overlayOffset = 224;
      int menuOffset = 50;
      if (menuStyle == chosenMenuStyle.horizontalContextStyle || horizontalContextLabels.Checked)
        menuOffset = 60;

      int menuTop = int.Parse(txtMenuPos.Text) + basicHomeValues.offsetMymenu + menuOffset;

      if (enableTwitter.Checked)
        menuTop = int.Parse(txtMenuPos.Text) - basicHomeValues.offsetTwitter + 30;

      overlayYpos = menuTop - overlayOffset;
      if (mrTVSeriesSummStyle == mostRecentTVSeriesSummaryStyle.poster)
      {
        buildTVSeriesSummaryFile(overlayYpos, mostRecentTVSeriesSummaryStyle.poster);
        writeXMLFile("basichome.recentlyadded.tvseries.HSum.xml");
      }
      if (mrTVSeriesSummStyle == mostRecentTVSeriesSummaryStyle.fanart)
      {
        buildTVSeriesSummaryFile(overlayYpos, mostRecentTVSeriesSummaryStyle.fanart);
        if (cbTVSeriesRecentWatched.Checked)
          mostRecentTVSeriesWatched();

        writeXMLFile("basichome.recentlyadded.tvseries.HSum2.xml");
      }


    }

    #endregion

    #region TVSeries Summary Builder

    void buildTVSeriesSummaryFile(int overlayYpos, mostRecentTVSeriesSummaryStyle sumStyle)
    {
      #region Summary Style 1

      if (sumStyle == mostRecentTVSeriesSummaryStyle.poster)
      {
        // Build the file
        xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>" +
              "<window>" +
                "<controls>" +
                  "<control>" +
                    "<description>GROUP: RecentlyAdded Series 3</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#StreamedMP.recentlyAdded.series3.thumb,#)</visible>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,200" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                    "<control>" +
                      "<description>Series 3 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>861</posX>" +
                      "<posY>" + (overlayYpos).ToString() + "</posY>" +
                      "<width>135</width>" +
                      "<height>220</height>" +
                      "<texture>pic_preview_thumb_background.png</texture>" +
                      "<colordiffuse>99FFFFFF</colordiffuse>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 3 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>871</posX>" +
                      "<posY>" + (overlayYpos + 11).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<height>170</height>" +
                      "<keepaspectratio>true</keepaspectratio>" +
                      "<texture>#StreamedMP.recentlyAdded.series3.thumb</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 3 episode</description>" +
                      "<type>label</type>" +
                      "<id>0</id>" +
                      "<posX>871</posX>" +
                      "<posY>" + (overlayYpos + 194).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<label>#StreamedMP.recentlyAdded.series3.seasonx#StreamedMP.recentlyAdded.series3.episodenumber</label>" +
                      "<font>mediastream10tc</font>" +
                      "<align>center</align>" +
                      "<textcolor>White</textcolor>" +
                    "</control>" +
                  "</control>" +
                  "<control>" +
                    "<description>GROUP: RecentlyAdded Series 2</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#StreamedMP.recentlyAdded.series2.thumb,#)</visible>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,200" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                    "<control>" +
                      "<description>Series 2 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1001</posX>" +
                      "<posY>" + (overlayYpos).ToString() + "</posY>" +
                      "<width>135</width>" +
                      "<height>220</height>" +
                      "<texture>pic_preview_thumb_background.png</texture>" +
                      "<colordiffuse>99FFFFFF</colordiffuse>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 2 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1011</posX>" +
                      "<posY>" + (overlayYpos + 11).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<height>170</height>" +
                      "<keepaspectratio>true</keepaspectratio>" +
                      "<texture>#StreamedMP.recentlyAdded.series2.thumb</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 2 episode</description>" +
                      "<type>label</type>" +
                      "<id>0</id>" +
                      "<posX>1011</posX>" +
                      "<posY>" + (overlayYpos + 194).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<label>#StreamedMP.recentlyAdded.series2.seasonx#StreamedMP.recentlyAdded.series2.episodenumber</label>" +
                      "<font>mediastream10tc</font>" +
                      "<align>center</align>" +
                      "<textcolor>White</textcolor>" +
                    "</control>" +
                  "</control>" +
                  "<control>" +
                    "<description>GROUP: RecentlyAdded Series 1</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#StreamedMP.recentlyAdded.series1.thumb,#)</visible>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,200" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                    "<control>" +
                      "<description>Series 3 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1141</posX>" +
                      "<posY>" + (overlayYpos).ToString() + "</posY>" +
                      "<width>135</width>" +
                      "<height>220</height>" +
                      "<texture>pic_preview_thumb_background.png</texture>" +
                      "<colordiffuse>99FFFFFF</colordiffuse>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 3 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1151</posX>" +
                      "<posY>" + (overlayYpos + 11).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<height>170</height>" +
                      "<keepaspectratio>true</keepaspectratio>" +
                      "<texture>#StreamedMP.recentlyAdded.series1.thumb</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 3 episode</description>" +
                      "<type>label</type>" +
                      "<id>0</id>" +
                      "<posX>1151</posX>" +
                      "<posY>" + (overlayYpos + 194).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<label>#StreamedMP.recentlyAdded.series1.seasonx#StreamedMP.recentlyAdded.series1.episodenumber</label>" +
                      "<font>mediastream10tc</font>" +
                      "<align>center</align>" +
                      "<textcolor>White</textcolor>" +
                    "</control>" +
                  "</control>" +
                "</controls>" +
              "</window>";
      }

      #endregion

      #region Summary Style 2

      if (sumStyle == mostRecentTVSeriesSummaryStyle.fanart)
      {
        string fanartProperty = "#StreamedMP.MostRecentImageFanart"; 
        string mrSeriesNameFont = tvSeriesOptions.mrSeriesFont;
        string mrEpisodeFont = tvSeriesOptions.mrEpisodeFont;
        bool mrSeriesTitleLast = tvSeriesOptions.mrTitleLast;

        if (!mostRecentTVSeriesCycleFanart)
          fanartProperty = "#StreamedMP.recentlyAdded.series1.fanart";

        xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>\n" +
              "<window>\n" +
                "<controls>\n" +
                  "<control>\n" +
                    "<description>GROUP: RecentlyAdded Series</description>\n" +
                    "<type>group</type>\n" +
                    "<dimColor>0xffffffff</dimColor>\n" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+![string.starts(#StreamedMP.recentlyAdded.series1.fanart,#)|string.starts(#StreamedMP.recentlyAdded.series1.thumb,#)]</visible>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
                    "<control>\n" +
                    "<description>Series 1 BG</description>\n" +
                    "<posX>" + baseXPosAdded.ToString() + "</posX>\n" +
                    "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>306</width>\n" +
                    "<height>320</height>\n" +
                    "<texture>recentsummoverlaybg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Header label</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 20).ToString() + "</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#StreamedMP.LatestEpisodes</label>\n" +
                    "<font>mediastream10tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>      " +
                  "<control>\n" +
                    "<description>Series 1 name</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 195).ToString() + "</posY>\n" +
                    "<width>258</width>\n" +
                    "<scrollStartDelaySec>30</scrollStartDelaySec>";
        if (mrSeriesTitleLast)
          xml += "<label>#StreamedMP.MostRecent.Added.1.SEFormat - #StreamedMP.recentlyAdded.series1.title</label>\n";
        else
          xml += "<label>#StreamedMP.recentlyAdded.series1.title - #StreamedMP.MostRecent.Added.1.SEFormat</label>\n";
        xml += "<font>" + mrSeriesNameFont + "</font>\n" +
        "<textcolor>White</textcolor>\n" +
      "</control>\n" +
      "<control>\n" +
        "<description>Series 1 title</description>\n" +
        "<type>fadelabel</type>\n" +
        "<id>0</id>\n" +
        "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
        "<posY>" + (baseYPosAdded + 212).ToString() + "</posY>\n" +
        "<width>255</width>\n" +
        "<scrollStartDelaySec>30</scrollStartDelaySec>" +
        "<label>#StreamedMP.recentlyAdded.series1.episodetitle</label>\n" +
        "<font>" + mrEpisodeFont + "</font>\n" +
        "<textcolor>White</textcolor>\n" +
      "</control>" +
      "<control>\n" +
        "<description>Series 1 thumb/fanart</description>\n" +
        "<type>image</type>\n" +
        "<id>0</id>\n" +
        "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
        "<posY>" + (baseYPosAdded + 42).ToString() + "</posY>\n" +
        "<width>268</width>\n" +
        "<height>151</height>\n" +
        "<keepaspectratio>true</keepaspectratio>\n" +
        "<texture>" + fanartProperty + "</texture>\n" +
      "</control>\n" +
    "</control>\n" +
    "<control>\n" +
      "<description>GROUP: RecentlyAdded Series</description>\n" +
      "<type>group</type>\n" +
      "<dimColor>0xffffffff</dimColor>\n" +
      "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+![string.starts(#StreamedMP.recentlyAdded.series2.fanart,#)|string.starts(#StreamedMP.recentlyAdded.series2.thumb,#)]</visible>" +
      "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
      "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
      "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
      "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
      "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
      "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
      "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
      "<control>\n" +
        "<description>Series 2 name</description>\n" +
        "<type>fadelabel</type>\n" +
        "<id>0</id>\n" +
        "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
        "<posY>" + (baseYPosAdded + 230).ToString() + "</posY>\n" +
        "<width>258</width>\n" + 
        "<scrollStartDelaySec>30</scrollStartDelaySec>";
        if (mrSeriesTitleLast)
          xml += "<label>#StreamedMP.MostRecent.Added.2.SEFormat - #StreamedMP.recentlyAdded.series2.title</label>\n";
        else
          xml += "<label>#StreamedMP.recentlyAdded.series2.title - #StreamedMP.MostRecent.Added.2.SEFormat</label>\n";
        xml += "<font>" + mrSeriesNameFont + "</font>\n" +
        "<textcolor>White</textcolor>\n" +
      "</control>\n" +
      "<control>\n" +
        "<description>Series 2 title</description>\n" +
        "<type>fadelabel</type>\n" +
        "<id>0</id>\n" +
        "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
        "<posY>" + (baseYPosAdded + 247).ToString() + "</posY>\n" +
        "<width>255</width>\n" +
        "<scrollStartDelaySec>30</scrollStartDelaySec>" +
        "<label>#StreamedMP.recentlyAdded.series2.episodetitle</label>\n" +
        "<font>" + mrEpisodeFont + "</font>\n" +
        "<textcolor>White</textcolor>\n" +
      "</control>\n" +
    "</control>\n" +
    "<control>\n" +
      "<description>GROUP: RecentlyAdded Series</description>\n" +
      "<type>group</type>\n" +
      "<dimColor>0xffffffff</dimColor>\n" +
      "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+![string.starts(#StreamedMP.recentlyAdded.series3.fanart,#)|string.starts(#StreamedMP.recentlyAdded.series3.thumb,#)]</visible>" +
      "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
      "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
      "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
      "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
      "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
      "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
      "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
      "<control>\n" +
        "<type>fadelabel</type>\n" +
        "<id>0</id>\n" +
        "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
        "<posY>" + (baseYPosAdded + 265).ToString() + "</posY>\n" +
        "<width>258</width>\n" +
        "<scrollStartDelaySec>30</scrollStartDelaySec>";
        if (mrSeriesTitleLast)
          xml += "<label>#StreamedMP.MostRecent.Added.3.SEFormat - #StreamedMP.recentlyAdded.series3.title</label>\n";
        else
          xml += "<label>#StreamedMP.recentlyAdded.series3.title - #StreamedMP.MostRecent.Added.3.SEFormat</label>\n";
        xml += "<font>" + mrSeriesNameFont + "</font>\n" +
        "<textcolor>White</textcolor>\n" +
      "</control>\n" +
      "<control>\n" +
        "<description>Series 3 title</description>\n" +
        "<type>fadelabel</type>\n" +
        "<id>0</id>\n" +
        "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
        "<posY>" + (baseYPosAdded + 282).ToString() + "</posY>\n" +
        "<width>255</width>\n" +
        "<scrollStartDelaySec>30</scrollStartDelaySec>" +
        "<label>#StreamedMP.recentlyAdded.series3.episodetitle</label>\n" +
        "<font>" + mrEpisodeFont + "</font>\n" +
        "<textcolor>White</textcolor>\n" +
      "</control>\n" +
    "</control>\n";
        if (!cbTVSeriesRecentWatched.Checked)
        {
          xml += "</controls>\n" +
         "</window>";
        }

      }

      #endregion
    }
    #endregion

    #region MovingPictures Vertical Full

    void verticalMovingPicturesFull()
    {
      xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>\n" +
            "<window>\n" +
              "<controls>\n" +
                "<control>\n" +
                  "<description>GROUP: RecentlyAdded Series</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+!string.starts(#StreamedMP.recentlyAdded.movie1.thumb,#)</visible>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "<control>\n" +
                    "<description>Series 1 BG</description>\n" +
                    "<posX>890</posX>\n" +
                    "<posY>70</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>385</width>\n" +
                    "<height>180</height>\n" +
                    "<texture>recentfullbg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 thumb</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>900</posX>\n" +
                    "<posY>79</posY>\n" +
                    "<width>112</width>\n" +
                    "<height>165</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>#StreamedMP.recentlyAdded.movie1.thumb</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 Rounded Cover</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>892</posX>\n" +
                    "<posY>71</posY>\n" +
                    "<height>181</height>\n" +
                    "<width>129</width>\n" +
                    "<texture>round.poster.frame.noreflection.png</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 name</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>98</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie1.title</label>\n" +
                    "<font>mediastream12tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                    "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 title</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>151</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie1.runtime</label>\n" +
                    "<font>mediastream12</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 episode</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>206</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie1.certification</label>\n" +
                    "<font>mediastream10</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                "</control>\n" +
                "<control>\n" +
                  "<description>GROUP: RecentlyAdded Series</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+!string.starts(#StreamedMP.recentlyAdded.movie2.thumb,#)</visible>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "<control>\n" +
                    "<description>Series 2 BG</description>\n" +
                    "<posX>890</posX>\n" +
                    "<posY>270</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>385</width>\n" +
                    "<height>180</height>\n" +
                    "<texture>recentfullbg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 thumb</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>902</posX>\n" +
                    "<posY>279</posY>\n" +
                    "<width>112</width>\n" +
                    "<height>165</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>#StreamedMP.recentlyAdded.movie2.thumb</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 Rounded Cover</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>892</posX>\n" +
                    "<posY>270</posY>\n" +
                    "<height>181</height>\n" +
                    "<width>129</width>\n" +
                    "<texture>round.poster.frame.noreflection.png</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 name</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>299</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie2.title</label>\n" +
                    "<font>mediastream12tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                    "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 title</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>352</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie2.runtime</label>\n" +
                    "<font>mediastream12</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 episode</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>405</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie2.certification</label>\n" +
                    "<font>mediastream10</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                "</control>\n" +
                "<control>\n" +
                  "<description>GROUP: RecentlyAdded Series</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+!string.starts(#StreamedMP.recentlyAdded.movie3.thumb,#)</visible>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "<control>\n" +
                    "<description>Series 3 BG</description>\n" +
                    "<posX>890</posX>\n" +
                    "<posY>470</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>385</width>\n" +
                    "<height>180</height>\n" +
                    "<texture>recentfullbg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 thumb</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>901</posX>\n" +
                    "<posY>478</posY>\n" +
                    "<width>112</width>\n" +
                    "<height>165</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>#StreamedMP.recentlyAdded.movie3.thumb</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 Rounded Cover</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>892</posX>\n" +
                    "<posY>470</posY>\n" +
                    "<height>181</height>\n" +
                    "<width>129</width>\n" +
                    "<texture>round.poster.frame.noreflection.png</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 name</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>498</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie3.title</label>\n" +
                    "<font>mediastream12tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 title</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>551</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie3.runtime</label>\n" +
                    "<font>mediastream12</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                    "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 episode</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>604</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie3.certification</label>\n" +
                    "<font>mediastream10</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                "</control>\n" +
              "</controls>\n" +
            "</window>";

      writeXMLFile("basichome.recentlyadded.movpics.VFull.xml");

    }

    #endregion

    #region MovingPictures Vertical Summary

    void verticalMovingPicturesSummary()
    {
      if (mrMovPicsSummStyle == mostRecentMovPicsSummaryStyle.poster)
      {
        buildMovingPicturesSummaryFile(475, mostRecentMovPicsSummaryStyle.poster);
        writeXMLFile("basichome.recentlyadded.movpics.VSum.xml");
      }
      if (mrMovPicsSummStyle == mostRecentMovPicsSummaryStyle.fanart)
      {
        buildMovingPicturesSummaryFile(475, mostRecentMovPicsSummaryStyle.fanart);
        if (cbMovPicsRecentWatched.Checked)
          mostRecentMoviesWatched();
  
        writeXMLFile("basichome.recentlyadded.movpics.VSum2.xml");
      }
    }

    #endregion

    #region MovingPictures Horizontal Full

    void horizonalMovingPicturesFull()
    {
      //
      // Work out the Ypos
      //
      int overlayYpos = 0;
      int maxYpos = 536;
      int overlayOffset = 184;
      int hdOffset = 0;

      int menuTop = int.Parse(txtMenuPos.Text) + basicHomeValues.offsetMymenu + 54;
      int menuBot = int.Parse(txtMenuPos.Text) + basicHomeValues.offsetSubmenu;

      if (screenres == screenResolutionType.res1920x1080)
        hdOffset = 0;
      else
        hdOffset = 0;

      if (enableTwitter.Checked)
        menuTop = int.Parse(txtMenuPos.Text) - basicHomeValues.offsetTwitter + 30;

      if (enableRssfeed.Checked && infoserviceOptions.Enabled)
        menuBot += (basicHomeValues.subMenuHeight - 30);

      if (menuBot > maxYpos)
      {
        overlayYpos = menuTop - overlayOffset;
        if (screenres == screenResolutionType.res1920x1080)
          hdOffset -= 54;
      }
      else
        overlayYpos = 536;

      // Build the file
      xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>" +
            "<window>" +
              "<controls>" +
                "<control>" +
                  "<description>GROUP: RecentlyAdded Movie 1</description>" +
                  "<type>group</type>" +
                  "<dimColor>0xffffffff</dimColor>" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+!string.starts(#StreamedMP.recentlyAdded.movie1.thumb,#)</visible>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,300" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                  "<control>" +
                    "<description>Mivie 1 BG</description>" +
                    "<posX>28</posX>" +
                    "<posY>" + (overlayYpos + 2).ToString() + "</posY>" +
                    "<type>image</type>" +
                    "<id>6777</id>" +
                    "<width>385</width>" +
                    "<height>180</height>" +
                    "<texture>recentfullbg.png</texture>" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 1 Thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>36</posX>" +
                    "<posY>" + (overlayYpos + 10).ToString() + "</posY>" +
                    "<height>163</height>" +
                    "<width>111</width>" +
                    "<keepaspectratio>true</keepaspectratio>" +
                    "<texture>#StreamedMP.recentlyAdded.movie1.thumb</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 1 Rounded Cover</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>26</posX>" +
                    "<posY>" + (overlayYpos + 2).ToString() + "</posY>" +
                    "<height>181</height>" +
                    "<width>129</width>" +
                    "<texture>round.poster.frame.noreflection.png</texture>" +
                  "</control>      " +
                "<control>" +
                    "<description>Movie 2 Title</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>152</posX>" +
                    "<posY>" + (overlayYpos + 29).ToString() + "</posY>" +
                    "<width>258</width>" +
                    "<label>#StreamedMP.recentlyAdded.movie1.title</label>" +
                    "<font>mediastream12tc</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 1 Runtime</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>152</posX>" +
                    "<posY>" + (overlayYpos + 82).ToString() + "</posY>" +
                    "<width>255</width>" +
                    "<label>#StreamedMP.recentlyAdded.movie1.runtime</label>" +
                    "<font>mediastream12</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 1 Certification</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>152</posX>" +
                    "<posY>" + (overlayYpos + 139).ToString() + "</posY>" +
                    "<width>255</width>" +
                    "<label>#StreamedMP.recentlyAdded.movie1.certification</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                "</control>" +
                "<control>" +
                  "<description>GROUP: RecentlyAdded Movie 2</description>" +
                  "<type>group</type>" +
                  "<dimColor>0xffffffff</dimColor>" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+!string.starts(#StreamedMP.recentlyAdded.movie2.thumb,#)</visible>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,300" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                  "<control>" +
                    "<description>Movie 2 BG</description>" +
                    "<posX>442</posX>" +
                    "<posY>" + (overlayYpos + 2).ToString() + "</posY>" +
                    "<type>image</type>" +
                    "<id>6777</id>" +
                    "<width>385</width>" +
                    "<height>180</height>" +
                    "<texture>recentfullbg.png</texture>" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 2 thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>452</posX>" +
                    "<posY>" + (overlayYpos + 10).ToString() + "</posY>" +
                    "<height>163</height>" +
                    "<width>111</width>" +
                    "<keepaspectratio>true</keepaspectratio>" +
                    "<texture>#StreamedMP.recentlyAdded.movie2.thumb</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie Rounded Cover</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>440</posX>" +
                    "<posY>" + (overlayYpos + 1).ToString() + "</posY>" +
                    "<height>181</height>" +
                    "<width>127</width>" +
                    "<texture>round.poster.frame.noreflection.png</texture>" +
                  "</control>      " +
                  "<control>" +
                    "<description>Movie 2 Title</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>568</posX>" +
                    "<posY>" + (overlayYpos + 29).ToString() + "</posY>" +
                    "<width>258</width>" +
                    "<label>#StreamedMP.recentlyAdded.movie2.title</label>" +
                    "<font>mediastream12tc</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 2 Runtime</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>565</posX>" +
                    "<posY>" + (overlayYpos + 82).ToString() + "</posY>" +
                    "<width>255</width>" +
                    "<label>#StreamedMP.recentlyAdded.movie2.runtime</label>" +
                    "<font>mediastream12</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 2 Certification</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>565</posX>" +
                    "<posY>" + (overlayYpos + 139).ToString() + "</posY>" +
                    "<width>258</width>" +
                    "<label>#StreamedMP.recentlyAdded.movie2.certification</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                "</control>" +
                "<control>" +
                  "<description>GROUP: RecentlyAdded Movie 3</description>" +
                  "<type>group</type>" +
                  "<dimColor>0xffffffff</dimColor>" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+!string.starts(#StreamedMP.recentlyAdded.movie3.thumb,#)</visible>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,300" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                  "<control>" +
                    "<description>Movie 3 BG</description>" +
                    "<posX>855</posX>" +
                    "<posY>" + (overlayYpos + 2).ToString() + "</posY>" +
                    "<type>image</type>" +
                    "<id>6777</id>" +
                    "<width>385</width>" +
                    "<height>180</height>" +
                    "<texture>recentfullbg.png</texture>" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 3 Thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>866</posX>" +
                    "<posY>" + (overlayYpos + 10).ToString() + "</posY>" +
                    "<height>163</height>" +
                    "<width>111</width>" +
                    "<keepaspectratio>true</keepaspectratio>" +
                    "<texture>#StreamedMP.recentlyAdded.movie3.thumb</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 3 Rounded Cover</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>858</posX>" +
                    "<posY>" + (overlayYpos + 2).ToString() + "</posY>" +
                    "<height>181</height>" +
                    "<width>127</width>" +
                    "<texture>round.poster.frame.noreflection.png</texture>" +
                  "</control>      " +
                  "<control>" +
                    "<description>Movie 3 Title</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>981</posX>" +
                    "<posY>" + (overlayYpos + 29).ToString() + "</posY>" +
                    "<width>255</width>" +
                    "<label>#StreamedMP.recentlyAdded.movie3.title</label>" +
                    "<font>mediastream12tc</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 3 Runtime</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>981</posX>" +
                    "<posY>" + (overlayYpos + 82).ToString() + "</posY>" +
                    "<width>255</width>" +
                    "<label>#StreamedMP.recentlyAdded.movie2.runtime</label>" +
                    "<font>mediastream12</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 3 Certification</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>981</posX>" +
                    "<posY>" + (overlayYpos + 139).ToString() + "</posY>" +
                    "<width>255</width>" +
                    "<label>#StreamedMP.recentlyAdded.movie3.certification</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                "</control>" +
                "</controls>" +
            "</window>";

      writeXMLFile("basichome.recentlyadded.movpics.HFull.xml");

    }

    #endregion

    #region MovingPictures Horizontal Summary

    void horizontalMovingPicturesSummary()
    {
      //
      // Work out the Ypos
      //
      int overlayYpos = 0;
      int overlayOffset = 224;
      int menuOffset = 50;
      if (menuStyle == chosenMenuStyle.horizontalContextStyle || horizontalContextLabels.Checked)
        menuOffset = 60;

      int menuTop = int.Parse(txtMenuPos.Text) + basicHomeValues.offsetMymenu + menuOffset;

      if (enableTwitter.Checked)
        menuTop = int.Parse(txtMenuPos.Text) - basicHomeValues.offsetTwitter + 30;

      overlayYpos = menuTop - overlayOffset;
      if (mrMovPicsSummStyle == mostRecentMovPicsSummaryStyle.poster)
      {
        buildMovingPicturesSummaryFile(overlayYpos, mostRecentMovPicsSummaryStyle.poster);
        writeXMLFile("basichome.recentlyadded.movpics.HSum.xml");
      }
      if (mrMovPicsSummStyle == mostRecentMovPicsSummaryStyle.fanart)
      {
        buildMovingPicturesSummaryFile(overlayYpos, mostRecentMovPicsSummaryStyle.fanart);
        //if (cbMovPicsRecentWatched.Checked)
        //  mostRecentMoviesWatched();

        writeXMLFile("basichome.recentlyadded.movpics.HSum2.xml");
      }


    }

    #endregion

    #region MovingPictures Summary Builder

    void buildMovingPicturesSummaryFile(int sumStyle1OverlayYpos, mostRecentMovPicsSummaryStyle sumStyle)
    {
      #region Summary Style 1

      if (sumStyle == mostRecentMovPicsSummaryStyle.poster)
      {
        // Build the file
        xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>" +
              "<window>" +
                "<controls>" +
                  "<control>" +
                    "<description>GROUP: RecentlyAdded Movie 3</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+!string.starts(#StreamedMP.recentlyAdded.movie3.thumb,#)</visible>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,200" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                    "<control>" +
                      "<description>Series 3 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>861</posX>" +
                      "<posY>" + (sumStyle1OverlayYpos).ToString() + "</posY>" +
                      "<width>135</width>" +
                      "<height>220</height>" +
                      "<texture>pic_preview_thumb_background.png</texture>" +
                      "<colordiffuse>99FFFFFF</colordiffuse>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 3 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>871</posX>" +
                      "<posY>" + (sumStyle1OverlayYpos + 11).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<height>170</height>" +
                      "<keepaspectratio>true</keepaspectratio>" +
                      "<texture>#StreamedMP.recentlyAdded.movie3.thumb</texture>" +
                    "</control>" +
                  "<control>" +
                    "<description>Movie 1 Star Rating</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>871</posX>" +
                    "<posY>" + (sumStyle1OverlayYpos + 185).ToString() + "</posY>" +
                    "<width>115</width>" +
                    "<height>22</height>" +
                    "<texture>star#StreamedMP.recentlyAdded.movie3.score.png</texture>" +
                    "<align>left</align>" +
                  "</control>" +
                  "</control>" +
                  "<control>" +
                    "<description>GROUP: RecentlyAdded Series 2</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+!string.starts(#StreamedMP.recentlyAdded.movie2.thumb,#)</visible>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,200" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                    "<control>" +
                      "<description>Series 2 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1001</posX>" +
                      "<posY>" + (sumStyle1OverlayYpos).ToString() + "</posY>" +
                      "<width>135</width>" +
                      "<height>220</height>" +
                      "<texture>pic_preview_thumb_background.png</texture>" +
                      "<colordiffuse>99FFFFFF</colordiffuse>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 2 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1011</posX>" +
                      "<posY>" + (sumStyle1OverlayYpos + 11).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<height>170</height>" +
                      "<keepaspectratio>true</keepaspectratio>" +
                      "<texture>#StreamedMP.recentlyAdded.movie2.thumb</texture>" +
                    "</control>" +
                  "<control>" +
                    "<description>Movie 2 Star Rating</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>1011</posX>" +
                    "<posY>" + (sumStyle1OverlayYpos + 185).ToString() + "</posY>" +
                    "<width>115</width>" +
                    "<height>22</height>" +
                    "<texture>star#StreamedMP.recentlyAdded.movie2.score.png</texture>" +
                    "<align>left</align>" +
                  "</control>" +
                  "</control>" +
                  "<control>" +
                    "<description>GROUP: RecentlyAdded Series 1</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+!string.starts(#StreamedMP.recentlyAdded.movie1.thumb,#)</visible>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,200" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                    "<control>" +
                      "<description>Movie 1 background</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1141</posX>" +
                      "<posY>" + (sumStyle1OverlayYpos).ToString() + "</posY>" +
                      "<width>135</width>" +
                      "<height>220</height>" +
                      "<texture>pic_preview_thumb_background.png</texture>" +
                      "<colordiffuse>99FFFFFF</colordiffuse>" +
                    "</control>" +
                    "<control>" +
                      "<description>Movie 1 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1151</posX>" +
                      "<posY>" + (sumStyle1OverlayYpos + 11).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<height>170</height>" +
                      "<keepaspectratio>true</keepaspectratio>" +
                      "<texture>#StreamedMP.recentlyAdded.movie1.thumb</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Movie 1 Star rating</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1151</posX>" +
                      "<posY>" + (sumStyle1OverlayYpos + 185).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<height>22</height>" +
                      "<texture>star#StreamedMP.recentlyAdded.movie1.score.png</texture>" +
                      "<align>left</align>" +
                    "</control>" +
                  "</control>" +
                "</controls>" +
              "</window>";
      }

      #endregion

      #region Summary Style 2

      if (sumStyle == mostRecentMovPicsSummaryStyle.fanart)
      {
        bool mrSeriesTitleLast = tvSeriesOptions.mrTitleLast;
        
        int xPos = baseXPosAdded + 20;

        string mrMovieTitleFont = movPicsOptions.MovieTitleFont;
        string mrMovieDetailFont = movPicsOptions.MovieDetailFont;
        string alignTxt = "right";
        string fanartProperty = "#StreamedMP.recentlyAdded.movie1.fanart";

        if (cbCycleFanart.Checked)
          fanartProperty = "#StreamedMP.MostRecentMovPicsImageFanart";

        xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>\n" +
              "<window>\n" +
                "<controls>\n" +
                  "<control>\n" +
                    "<description>GROUP: RecentlyAdded Movie 1</description>\n" +
                    "<type>group</type>\n" +
                    "<dimColor>0xffffffff</dimColor>\n" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+![string.starts(#StreamedMP.recentlyAdded.movie1.fanart,#)|string.starts(#StreamedMP.recentlyAdded.movie1.thumb,#)]</visible>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
                    "<control>\n" +
                    "<description>Movie 1 BG</description>\n" +
                    "<posX>" + baseXPosAdded.ToString() + "</posX>\n" +
                    "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>306</width>\n" +
                    "<height>320</height>\n" +
                    "<texture>recentsummoverlaybg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Header label</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 20).ToString() + "</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#StreamedMP.LatestMovies</label>\n" +
                    "<font>mediastream10tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>      " +
                  "<control>\n" +
                    "<description>Movie 1 Title</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 195).ToString() + "</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie1.title</label>\n" +
                    "<textcolor>White</textcolor>\n" +
                    "<font>" + mrMovieTitleFont + "</font>" +
                    "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                  "</control>\n";

        if (!movPicsOptions.HideRuntime)
        {
          xml += "<control>\n" +
            "<description>Movie 1 Runtime</description>\n" +
            "<type>label</type>\n" +
            "<id>0</id>\n" +
            "<posX>" + xPos.ToString() + "</posX>\n" +
             "<posY>" + (baseYPosAdded + 212).ToString() + "</posY>\n" +
             "<width>257</width>\n" +
            "<label>#StreamedMP.recentlyAdded.movie1.runtime</label>\n" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<textcolor>White</textcolor>\n" +
          "</control>";
          xPos = 1180;
        }
        else
        {
          alignTxt = "left";
          xPos -= 5;
        }

        if (!movPicsOptions.HideCertification)
        {
          xml += "<control>\n" +
            "<description>Movie 1 Certification</description>\n" +
            "<type>label</type>\n" +
            "<id>0</id>\n" +
            "<posX>" + xPos.ToString() + "</posX>\n" +
            "<posY>" + (baseYPosAdded + 212).ToString() + "</posY>\n" +
            "<width>257</width>\n" +
            "<label>#StreamedMP.recentlyAdded.movie1.certification</label>\n" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<textcolor>White</textcolor>\n" +
            "<align>" + alignTxt + "</align>" +
          "</control>";
        }

        if (!movPicsOptions.HideRating)
        {
          if (movPicsOptions.UseTextRating)
          {
            xml += "<control>" +
              "<description>Movie 1 Star Rating Text</description>" +
              "<type>label</type>" +
              "<id>0</id>" +
              "<posX>" + (baseXPosAdded + 284).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosAdded + 215).ToString() + "</posY>\n" +
              "<width>70</width>" +
              "<height>13</height>" +
              "<font>" + mrMovieDetailFont + "</font>" +
              "<align>right</align>" +
              "<label>#StreamedMP.recentlyAdded.movie1.actualscore/10</label>" +
            "</control>";
          }
          else
          {
            xml += "<control>" +
              "<description>Movie 1 Star Rating Image</description>" +
              "<type>image</type>" +
              "<id>0</id>" +
              "<posX>" + (baseXPosAdded + 214).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosAdded + 215).ToString() + "</posY>\n" +
              "<width>70</width>" +
              "<height>13</height>" +
              "<texture>star#StreamedMP.recentlyAdded.movie1.score.png</texture>" +
            "</control>";
          }
        }

        xPos = baseXPosAdded + 20;
        xml += "<control>\n" +
                "<description>Movie 1 thumb/fanart</description>\n" +
                "<type>image</type>\n" +
                "<id>0</id>\n" +
                "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                "<posY>" + (baseYPosAdded + 42).ToString() + "</posY>\n" +
                "<width>268</width>\n" + 
                "<height>151</height>\n" +
                "<keepaspectratio>true</keepaspectratio>\n" +
                "<texture>" + fanartProperty + "</texture>\n" +
              "</control>\n" +
            "</control>\n" +
            "<control>\n" +
              "<description>GROUP: RecentlyAdded Movie 2</description>\n" +
              "<type>group</type>\n" +
              "<dimColor>0xffffffff</dimColor>\n" +
              "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+![string.starts(#StreamedMP.recentlyAdded.movie2.fanart,#)|string.starts(#StreamedMP.recentlyAdded.movie2.thumb,#)]</visible>" +
              "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
              "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
              "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
              "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
              "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
              "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
              "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
              "<control>\n" +
                "<description>Movie 2 Title</description>\n" +
                "<type>fadelabel</type>\n" +
                "<id>0</id>\n" +
                "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                "<posY>" + (baseYPosAdded + 230).ToString() + "</posY>\n" +
                "<width>258</width>\n" +
                "<label>#StreamedMP.recentlyAdded.movie2.title</label>\n" +
                "<font>" + mrMovieTitleFont + "</font>" +
                "<textcolor>White</textcolor>\n" +
                "<scrollStartDelaySec>20</scrollStartDelaySec>" +
              "</control>\n";

        if (!movPicsOptions.HideRuntime)
        {
          xml += "<control>\n" +
            "<description>Movie 2 Runtime</description>\n" +
            "<type>label</type>\n" +
            "<id>0</id>\n" +
            "<posX>" + xPos.ToString() + "</posX>\n" +
            "<posY>" + (baseYPosAdded + 247).ToString() + "</posY>\n" +
            "<width>257</width>\n" +
            "<label>#StreamedMP.recentlyAdded.movie2.runtime</label>\n" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<textcolor>White</textcolor>\n" +
          "</control>";
          xPos = baseXPosAdded + 204;
        }
        else
        {
          xPos -= 5;
        }

        if (!movPicsOptions.HideCertification)
        {
          xml += "<control>\n" +
            "<description>Movie 2 Certification</description>\n" +
            "<type>label</type>\n" +
            "<id>0</id>\n" +
            "<posX>" + xPos.ToString() + "</posX>\n" +
            "<posY>" + (baseYPosAdded + 247).ToString() + "</posY>\n" +
            "<width>257</width>\n" +
            "<label>#StreamedMP.recentlyAdded.movie2.certification</label>\n" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<textcolor>White</textcolor>\n" +
            "<align>" + alignTxt + "</align>" +
          "</control>";
        }

        if (!movPicsOptions.HideRating)
        {
          if (movPicsOptions.UseTextRating)
          {
            xml += "<control>" +
              "<description>Movie 2 Star Rating Text</description>" +
              "<type>label</type>" +
              "<id>0</id>" +
              "<posX>" + (baseXPosAdded + 284).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosAdded + 247).ToString() + "</posY>\n" +
              "<width>70</width>" +
              "<height>13</height>" +
              "<font>" + mrMovieDetailFont + "</font>" +
              "<align>right</align>" +
              "<label>#StreamedMP.recentlyAdded.movie2.actualscore/10</label>" +
            "</control>";
          }
          else
          {
            xml += "<control>" +
              "<description>Movie 2 Star Rating Image</description>" +
              "<type>image</type>" +
              "<id>0</id>" +
              "<posX>" + (baseXPosAdded + 214).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosAdded + 251).ToString() + "</posY>\n" +
              "<width>70</width>" +
              "<height>13</height>" +
              "<texture>star#StreamedMP.recentlyAdded.movie2.score.png</texture>" +
            "</control>";
          }
        }

        xPos = baseXPosAdded + 20;
        xml += "</control>\n" +
        "<control>\n" +
          "<description>GROUP: RecentlyAdded Movie 3</description>\n" +
          "<type>group</type>\n" +
          "<dimColor>0xffffffff</dimColor>\n" +
          "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+![string.starts(#StreamedMP.recentlyAdded.movie3.fanart,#)|string.starts(#StreamedMP.recentlyAdded.movie3.thumb,#)]</visible>" +
          "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
          "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
          "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
          "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
          "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
          "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
          "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
          "<control>\n" +
            "<type>fadelabel</type>\n" +
            "<id>0</id>\n" +
            "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
            "<posY>" + (baseYPosAdded + 265).ToString() + "</posY>\n" +
            "<width>258</width>\n" +
            "<label>#StreamedMP.recentlyAdded.movie3.title</label>\n" +
            "<textcolor>White</textcolor>\n" +
            "<font>" + mrMovieTitleFont + "</font>" +
            "<scrollStartDelaySec>20</scrollStartDelaySec>" +
          "</control>\n";


        if (!movPicsOptions.HideRuntime)
        {
          xml += "<control>\n" +
            "<description>Movie 3 Runtime</description>\n" +
            "<type>label</type>\n" +
            "<id>0</id>\n" +
            "<posX>" + xPos.ToString() + "</posX>\n" +
            "<posY>" + (baseYPosAdded + 282).ToString() + "</posY>\n" +
            "<width>257</width>\n" +
            "<label>#StreamedMP.recentlyAdded.movie3.runtime</label>\n" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<textcolor>White</textcolor>\n" +
          "</control>";
          xPos = baseXPosAdded + 204;
        }
        else
        {
          xPos -= 5;
        }

        if (!movPicsOptions.HideCertification)
        {
          xml += "<control>\n" +
            "<description>Movie 3 Certification</description>\n" +
            "<type>label</type>\n" +
            "<id>0</id>\n" +
            "<posX>" + xPos.ToString() + "</posX>\n" +
            "<posY>" + (baseYPosAdded + 282).ToString() + "</posY>\n" +
            "<width>257</width>\n" +
            "<label>#StreamedMP.recentlyAdded.movie3.certification</label>\n" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<textcolor>White</textcolor>\n" +
            "<align>" + alignTxt + "</align>" +
          "</control>";
        }

        if (!movPicsOptions.HideRating)
        {
          if (movPicsOptions.UseTextRating)
          {
            xml += "<control>" +
              "<description>Movie 3 Star Rating Text</description>" +
              "<type>label</type>" +
              "<id>0</id>" +
              "<posX>" + (baseXPosAdded + 284).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosAdded + 282).ToString() + "</posY>\n" +
              "<width>70</width>" +
              "<height>13</height>" +
              "<font>" + mrMovieDetailFont + "</font>" +
              "<align>right</align>" +
              "<label>#StreamedMP.recentlyAdded.movie3.actualscore/10</label>" +
            "</control>";
          }
          else
          {
            xml += "<control>" +
              "<description>Movie 3 Star Rating Image</description>" +
              "<type>image</type>" +
              "<id>0</id>" +
              "<posX>" + (baseXPosAdded + 214).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosAdded + 285).ToString() + "</posY>\n" +
              "<width>70</width>" +
              "<height>13</height>" +
              "<texture>star#StreamedMP.recentlyAdded.movie3.score.png</texture>" +
            "</control>";
          }
        }

        xml += "</control>\n";
        if (!cbMovPicsRecentWatched.Checked || menuStyle != chosenMenuStyle.verticalStyle)
        {
         xml += "</controls>\n" +
        "</window>";
        }
      }

      #endregion
    }

    #endregion

    #region MovingPictures Most Recent Watched

    void mostRecentMoviesWatched()
    {
        string mrMovieTitleFont = movPicsOptions.MovieTitleFont;
        string mrMovieDetailFont = movPicsOptions.MovieDetailFont;
        bool mrSeriesTitleLast = tvSeriesOptions.mrTitleLast;
        int xPos = 995;
        string alignTxt = "right";

        string fanartProperty = "#StreamedMP.recentlyWatched.movie1.fanart";

        if (cbCycleFanart.Checked)
          fanartProperty = "#StreamedMP.MostRecentMovPicsImageFanartWatched";

        xml +=    "<control>\n" +
                    "<description>GROUP: RecentlyWatched Movie 1</description>\n" +
                    "<type>group</type>\n" +
                    "<dimColor>0xffffffff</dimColor>\n" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+![string.starts(#StreamedMP.recentlyWatched.movie1.fanart,#)|string.starts(#StreamedMP.recentlyWatched.movie1.thumb,#)]</visible>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
                    "<control>\n" +
                    "<description>Movie 1 BG</description>\n" +
                    "<posX>" + baseXPosWatched.ToString() + "</posX>\n" +
                    "<posY>" + baseYPosWatched.ToString() + "</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>306</width>\n" +
                    "<height>320</height>\n" +
                    "<texture>recentsummoverlaybg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Header label</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosWatched + 20).ToString() + "</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>Recently Watched</label>\n" +
                    "<font>mediastream10tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>      " +
                  "<control>\n" +
                    "<description>Movie 1 Title</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosWatched + 195).ToString() + "</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#StreamedMP.recentlyWatched.movie1.title</label>\n" +
                    "<textcolor>White</textcolor>\n" +
                    "<font>" + mrMovieTitleFont + "</font>" +
                    "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                  "</control>\n";

        if (!movPicsOptions.HideRuntime)
        {
          xml += "<control>\n" +
            "<description>Movie 1 Runtime</description>\n" +
            "<type>label</type>\n" +
            "<id>0</id>\n" +
            "<posX>" + xPos.ToString() + "</posX>\n" +
            "<posY>" + (baseYPosWatched + 212).ToString() + "</posY>\n" +
            "<width>257</width>\n" +
            "<label>#StreamedMP.recentlyWatched.movie1.runtime</label>\n" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<textcolor>White</textcolor>\n" +
          "</control>";
          xPos = baseXPosWatched + 204;
        }
        else
        {
          alignTxt = "left";
          xPos -= 5;
        }

        if (!movPicsOptions.HideCertification)
        {
          xml += "<control>\n" +
            "<description>Movie 1 Certification</description>\n" +
            "<type>label</type>\n" +
            "<id>0</id>\n" +
            "<posX>" + xPos.ToString() + "</posX>\n" +
            "<posY>" + (baseYPosWatched + 212).ToString() + "</posY>\n" +
            "<width>257</width>\n" +
            "<label>#StreamedMP.recentlyWatched.movie1.certification</label>\n" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<textcolor>White</textcolor>\n" +
            "<align>" + alignTxt + "</align>" +
          "</control>";
        }

        if (!movPicsOptions.HideRating)
        {
          if (movPicsOptions.UseTextRating)
          {
            xml += "<control>" +
              "<description>Movie 1 Star Rating Text</description>" +
              "<type>label</type>" +
              "<id>0</id>" +
              "<posX>" + (baseXPosWatched + 284).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosWatched + 215).ToString() + "</posY>\n" +
              "<width>70</width>" +
              "<height>13</height>" +
              "<font>" + mrMovieDetailFont + "</font>" +
              "<align>right</align>" +
              "<label>#StreamedMP.recentlyWatched.movie1.actualscore/10</label>" +
            "</control>";
          }
          else
          {
            xml += "<control>" +
              "<description>Movie 1 Star Rating Image</description>" +
              "<type>image</type>" +
              "<id>0</id>" +
              "<posX>" + (baseXPosWatched + 214).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosWatched + 215).ToString() + "</posY>\n" +
              "<width>70</width>" +
              "<height>13</height>" +
              "<texture>star#StreamedMP.recentlyWatched.movie1.score.png</texture>" +
            "</control>";
          }
        }

        xPos = baseXPosWatched + 19;
        xml += "<control>\n" +
                "<description>Movie 1 thumb/fanart</description>\n" +
                "<type>image</type>\n" +
                "<id>0</id>\n" +
                "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
                "<posY>" + (baseYPosWatched + 42).ToString() + "</posY>\n" +
                "<width>268</width>\n" +
                "<height>151</height>\n" +
                "<keepaspectratio>true</keepaspectratio>\n" +
                "<texture>" + fanartProperty + "</texture>\n" +
              "</control>\n" +
            "</control>\n" +
            "<control>\n" +
              "<description>GROUP: recentlyWatched Movie 2</description>\n" +
              "<type>group</type>\n" +
              "<dimColor>0xffffffff</dimColor>\n" +
              "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+![string.starts(#StreamedMP.recentlyWatched.movie2.fanart,#)|string.starts(#StreamedMP.recentlyWatched.movie2.thumb,#)]</visible>" +
              "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
              "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
              "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
              "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
              "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
              "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
              "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
              "<control>\n" +
                "<description>Movie 2 Title</description>\n" +
                "<type>fadelabel</type>\n" +
                "<id>0</id>\n" +
                "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
                "<posY>" + (baseYPosWatched + 230).ToString() + "</posY>\n" +
                "<width>258</width>\n" +
                "<label>#StreamedMP.recentlyWatched.movie2.title</label>\n" +
                "<font>" + mrMovieTitleFont + "</font>" +
                "<textcolor>White</textcolor>\n" +
                "<scrollStartDelaySec>20</scrollStartDelaySec>" +
              "</control>\n";

        if (!movPicsOptions.HideRuntime)
        {
          xml += "<control>\n" +
            "<description>Movie 2 Runtime</description>\n" +
            "<type>label</type>\n" +
            "<id>0</id>\n" +
            "<posX>" + xPos.ToString() + "</posX>\n" +
            "<posY>" + (baseYPosWatched + 247).ToString() + "</posY>\n" +
            "<width>257</width>\n" +
            "<label>#StreamedMP.recentlyWatched.movie2.runtime</label>\n" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<textcolor>White</textcolor>\n" +
          "</control>";
          xPos = baseXPosWatched + 204;
        }
        else
        {
          xPos -= 5;
        }

        if (!movPicsOptions.HideCertification)
        {
          xml += "<control>\n" +
            "<description>Movie 2 Certification</description>\n" +
            "<type>label</type>\n" +
            "<id>0</id>\n" +
            "<posX>" + xPos.ToString() + "</posX>\n" +
            "<posY>" + (baseYPosWatched + 247).ToString() + "</posY>\n" +
            "<width>257</width>\n" +
            "<label>#StreamedMP.recentlyWatched.movie2.certification</label>\n" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<textcolor>White</textcolor>\n" +
            "<align>" + alignTxt + "</align>" +
          "</control>";
        }

        if (!movPicsOptions.HideRating)
        {
          if (movPicsOptions.UseTextRating)
          {
            xml += "<control>" +
              "<description>Movie 2 Star Rating Text</description>" +
              "<type>label</type>" +
              "<id>0</id>" +
              "<posX>" + (baseXPosWatched + 284).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosWatched + 247).ToString() + "</posY>\n" +
              "<width>70</width>" +
              "<height>13</height>" +
              "<font>" + mrMovieDetailFont + "</font>" +
              "<align>right</align>" +
              "<label>#StreamedMP.recentlyWatched.movie2.actualscore/10</label>" +
            "</control>";
          }
          else
          {
            xml += "<control>" +
              "<description>Movie 2 Star Rating Image</description>" +
              "<type>image</type>" +
              "<id>0</id>" +
              "<posX>" + (baseXPosWatched + 214).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosWatched + 251).ToString() + "</posY>\n" +
              "<width>70</width>" +
              "<height>13</height>" +
              "<texture>star#StreamedMP.recentlyWatched.movie2.score.png</texture>" +
            "</control>";
          }
        }

        xPos = baseXPosWatched + 19;
        xml += "</control>\n" +
        "<control>\n" +
          "<description>GROUP: RecentlyAdded Movie 3</description>\n" +
          "<type>group</type>\n" +
          "<dimColor>0xffffffff</dimColor>\n" +
          "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+![string.starts(#StreamedMP.recentlyWatched.movie3.fanart,#)|string.starts(#StreamedMP.recentlyWatched.movie3.thumb,#)]</visible>" +
          "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
          "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
          "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
          "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
          "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
          "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
          "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
          "<control>\n" +
            "<type>fadelabel</type>\n" +
            "<id>0</id>\n" +
            "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
            "<posY>" + (baseYPosWatched + 265).ToString() + "</posY>\n" +
            "<width>258</width>\n" +
            "<label>#StreamedMP.recentlyWatched.movie3.title</label>\n" +
            "<textcolor>White</textcolor>\n" +
            "<font>" + mrMovieTitleFont + "</font>" +
            "<scrollStartDelaySec>20</scrollStartDelaySec>" +
          "</control>\n";


        if (!movPicsOptions.HideRuntime)
        {
          xml += "<control>\n" +
            "<description>Movie 3 Runtime</description>\n" +
            "<type>label</type>\n" +
            "<id>0</id>\n" +
            "<posX>" + xPos.ToString() + "</posX>\n" +
            "<posY>" + (baseYPosWatched + 282).ToString() + "</posY>\n" +
            "<width>257</width>\n" +
            "<label>#StreamedMP.recentlyWatched.movie3.runtime</label>\n" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<textcolor>White</textcolor>\n" +
          "</control>";
          xPos = baseXPosWatched + 204;
        }
        else
        {
          xPos -= 5;
        }

        if (!movPicsOptions.HideCertification)
        {
          xml += "<control>\n" +
            "<description>Movie 3 Certification</description>\n" +
            "<type>label</type>\n" +
            "<id>0</id>\n" +
            "<posX>" + xPos.ToString() + "</posX>\n" +
            "<posY>" + (baseYPosWatched + 282).ToString() + "</posY>\n" +
            "<width>257</width>\n" +
            "<label>#StreamedMP.recentlyWatched.movie3.certification</label>\n" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<textcolor>White</textcolor>\n" +
            "<align>" + alignTxt + "</align>" +
          "</control>";
        }

        if (!movPicsOptions.HideRating)
        {
          if (movPicsOptions.UseTextRating)
          {
            xml += "<control>" +
              "<description>Movie 3 Star Rating Text</description>" +
              "<type>label</type>" +
              "<id>0</id>" +
              "<posX>" + (baseXPosWatched + 284).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosWatched + 282).ToString() + "</posY>\n" +
              "<width>70</width>" +
              "<height>13</height>" +
              "<font>" + mrMovieDetailFont + "</font>" +
              "<align>right</align>" +
              "<label>#StreamedMP.recentlyWatched.movie3.actualscore/10</label>" +
            "</control>";
          }
          else
          {
            xml += "<control>" +
              "<description>Movie 3 Star Rating Image</description>" +
              "<type>image</type>" +
              "<id>0</id>" +
              "<posX>" + (baseXPosWatched + 214).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosWatched + 285).ToString() + "</posY>\n" +
              "<width>70</width>" +
              "<height>13</height>" +
              "<texture>star#StreamedMP.recentlyWatched.movie3.score.png</texture>" +
            "</control>";
          }
        }

        xml += "</control>\n" +
      "</controls>\n" +
    "</window>";
    }

    #endregion

    #region TVSeries Most Recent Watched

    void mostRecentTVSeriesWatched()
    {
        string fanartProperty = "#StreamedMP.MostRecentImageFanartWatched";
        string mrSeriesNameFont = tvSeriesOptions.mrSeriesFont;
        string mrEpisodeFont = tvSeriesOptions.mrEpisodeFont;
        bool mrSeriesTitleLast = tvSeriesOptions.mrTitleLast;

        if (!mostRecentTVSeriesCycleFanart)
          fanartProperty = "#StreamedMP.recentlyWatched.series1.fanart";

        xml += "<control>\n" +
                  "<description>GROUP: RecentlyWatched Series</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+![string.starts(#StreamedMP.recentlyWatched.series1.fanart,#)|string.starts(#StreamedMP.recentlyWatched.series1.thumb,#)]</visible>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n";
        if (baseXPosWatched > 640)
        {
           xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
        }
        else
        {
          xml +=  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
        }
          xml +=  "<control>\n" +
                  "<description>Series 1 BG</description>\n" +
                  "<posX>" + baseXPosWatched.ToString() + "</posX>\n" +
                  "<posY>" + baseYPosWatched.ToString() + "</posY>\n" +
                  "<type>image</type>\n" +
                  "<id>0</id>\n" +
                  "<width>306</width>\n" +
                  "<height>320</height>\n" +
                  "<texture>recentsummoverlaybg.png</texture>\n" +
                  "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                "</control>\n" +
                "<control>\n" +
                  "<description>Header label</description>\n" +
                  "<type>label</type>\n" +
                  "<id>0</id>\n" +
                  "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosWatched + 20).ToString() + "</posY>\n" +
                  "<width>258</width>\n" +
                  "<label>Recently Watched</label>\n" +
                  "<font>mediastream10tc</font>\n" +
                  "<textcolor>White</textcolor>\n" +
                "</control>      " +
                "<control>\n" +
                  "<description>Series 1 name</description>\n" +
                  "<type>fadelabel</type>\n" +
                  "<id>0</id>\n" +
                  "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosWatched + 195).ToString() + "</posY>\n" +
                  "<width>258</width>\n" +
                  "<scrollStartDelaySec>30</scrollStartDelaySec>";
      if (mrSeriesTitleLast)
        xml += "<label>#StreamedMP.MostRecent.Watched.1.SEFormat - #StreamedMP.recentlyWatched.series1.title</label>\n";
      else
        xml += "<label>#StreamedMP.recentlyWatched.series1.title - #StreamedMP.MostRecent.Watched.1.SEFormat</label>\n";
      xml += "<font>" + mrSeriesNameFont + "</font>\n" +
      "<textcolor>White</textcolor>\n" +
    "</control>\n" +
    "<control>\n" +
      "<description>Series 1 title</description>\n" +
      "<type>fadelabel</type>\n" +
      "<id>0</id>\n" +
      "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
      "<posY>" + (baseYPosWatched + 212).ToString() + "</posY>\n" +
      "<width>255</width>\n" +
      "<scrollStartDelaySec>30</scrollStartDelaySec>" +
      "<label>#StreamedMP.recentlyWatched.series1.episodetitle</label>\n" +
      "<font>" + mrEpisodeFont + "</font>\n" +
      "<textcolor>White</textcolor>\n" +
    "</control>" +
    "<control>\n" +
      "<description>Series 1 thumb/fanart</description>\n" +
      "<type>image</type>\n" +
      "<id>0</id>\n" +
      "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
      "<posY>" + (baseYPosWatched + 42).ToString() + "</posY>\n" +
      "<width>268</width>\n" +
      "<height>151</height>\n" +
      "<keepaspectratio>true</keepaspectratio>\n" +
      "<texture>" + fanartProperty + "</texture>\n" +
    "</control>\n" +
  "</control>\n" +
  "<control>\n" +
    "<description>GROUP: RecentlyWatched Series</description>\n" +
    "<type>group</type>\n" +
    "<dimColor>0xffffffff</dimColor>\n" +
    "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+![string.starts(#StreamedMP.recentlyWatched.series2.fanart,#)|string.starts(#StreamedMP.recentlyWatched.series2.thumb,#)]</visible>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n";
      if (baseXPosWatched > 640)
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      else
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      xml += "<control>\n" +
      "<description>Series 2 name</description>\n" +
      "<type>fadelabel</type>\n" +
      "<id>0</id>\n" +
      "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
      "<posY>" + (baseYPosWatched + 230).ToString() + "</posY>\n" +
      "<width>258</width>\n" +
      "<scrollStartDelaySec>30</scrollStartDelaySec>";
      if (mrSeriesTitleLast)
        xml += "<label>#StreamedMP.MostRecent.Watched.2.SEFormat - #StreamedMP.recentlyWatched.series2.title</label>\n";
      else
        xml += "<label>#StreamedMP.recentlyWatched.series2.title - #StreamedMP.MostRecent.Watched.2.SEFormat</label>\n";
      xml += "<font>" + mrSeriesNameFont + "</font>\n" +
      "<textcolor>White</textcolor>\n" +
    "</control>\n" +
    "<control>\n" +
      "<description>Series 2 title</description>\n" +
      "<type>fadelabel</type>\n" +
      "<id>0</id>\n" +
      "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
      "<posY>" + (baseYPosWatched + 247).ToString() + "</posY>\n" +
      "<width>255</width>\n" +
      "<scrollStartDelaySec>30</scrollStartDelaySec>" +
      "<label>#StreamedMP.recentlyWatched.series2.episodetitle</label>\n" +
      "<font>" + mrEpisodeFont + "</font>\n" +
      "<textcolor>White</textcolor>\n" +
    "</control>\n" +
  "</control>\n" +
  "<control>\n" +
    "<description>GROUP: RecentlyWatched Series</description>\n" +
    "<type>group</type>\n" +
    "<dimColor>0xffffffff</dimColor>\n" +
    "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+![string.starts(#StreamedMP.recentlyWatched.series3.fanart,#)|string.starts(#StreamedMP.recentlyWatched.series3.thumb,#)]</visible>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n";
      if (baseXPosWatched > 640)
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      else
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      xml += "<control>\n" +
      "<type>fadelabel</type>\n" +
      "<id>0</id>\n" +
      "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
      "<posY>" + (baseYPosWatched + 265).ToString() + "</posY>\n" +
      "<width>258</width>\n" +
      "<scrollStartDelaySec>30</scrollStartDelaySec>";
      if (mrSeriesTitleLast)
        xml += "<label>#StreamedMP.MostRecent.Watched.3.SEFormat - #StreamedMP.recentlyWatched.series3.title</label>\n";
      else
        xml += "<label>#StreamedMP.recentlyWatched.series3.title - #StreamedMP.MostRecent.Watched.3.SEFormat</label>\n";
      xml += "<font>" + mrSeriesNameFont + "</font>\n" +
      "<textcolor>White</textcolor>\n" +
    "</control>\n" +
    "<control>\n" +
      "<description>Series 3 title</description>\n" +
      "<type>fadelabel</type>\n" +
      "<id>0</id>\n" +
      "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
      "<posY>" + (baseYPosWatched + 282).ToString() + "</posY>\n" +
      "<width>255</width>\n" +
      "<scrollStartDelaySec>30</scrollStartDelaySec>" +
      "<label>#StreamedMP.recentlyWatched.series3.episodetitle</label>\n" +
      "<font>" + mrEpisodeFont + "</font>\n" +
      "<textcolor>White</textcolor>\n" +
    "</control>\n" +
  "</control>\n" +
  "</controls>\n" +
  "</window>";
    }

    #endregion

    #region Private Methods

    string mostRecentVisibleControls(isOverlayType isOverlay)
    {
      string visibleOn = null;
      //
      //Controls to display recent movies overlay
      //
      if (isOverlay == isOverlayType.MovPics)
      {
        foreach (menuItem menItem in menuItems)
        {
          if (menItem.showMostRecent == displayMostRecent.moviesAdded)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + menItem.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + menItem.id.ToString() + ")";
          }
          if (menItem.showMostRecent == displayMostRecent.moviesAddedWatched)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + menItem.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + menItem.id.ToString() + ")";
          }
          // Check Submenu Level 1
          if (menItem.subMenuLevel1.Count > 0)
          {
            for (int i = 0; i < menItem.subMenuLevel1.Count; i++)
            {
              if (menItem.subMenuLevel1[i].showMostRecent == displayMostRecent.moviesAddedWatched)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (menItem.subMenuLevel1ID + (i + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (menItem.subMenuLevel1ID + (i + 1)).ToString() + ")";
              }
              else
              {
                if (menItem.subMenuLevel1[i].showMostRecent == displayMostRecent.moviesAdded)
                {
                  if (visibleOn == null)
                    visibleOn = "[control.hasfocus(" + (menItem.subMenuLevel1ID + (i + 1)).ToString() + ")";
                  else
                    visibleOn += "|control.hasfocus(" + (menItem.subMenuLevel1ID + (i + 1)).ToString() + ")";
                }
                if (menItem.subMenuLevel1[i].showMostRecent == displayMostRecent.moviesWatched)
                {
                  if (visibleOn == null)
                    visibleOn = "[control.hasfocus(" + (menItem.subMenuLevel1ID + (i + 1)).ToString() + ")";
                  else
                    visibleOn += "|control.hasfocus(" + (menItem.subMenuLevel1ID + (i + 1)).ToString() + ")";
                }
              }
            }
          }
          // Check Submenu Level 2
          if (menItem.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < menItem.subMenuLevel2.Count; i++)
            {
              if (menItem.subMenuLevel2[i].showMostRecent == displayMostRecent.moviesAddedWatched)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (menItem.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (menItem.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
              }
              else
              {
                if (menItem.subMenuLevel2[i].showMostRecent == displayMostRecent.moviesAdded)
                {
                  if (visibleOn == null)
                    visibleOn = "[control.hasfocus(" + (menItem.subMenuLevel1ID + (i + 100 +1)).ToString() + ")";
                  else
                    visibleOn += "|control.hasfocus(" + (menItem.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                }
                if (menItem.subMenuLevel2[i].showMostRecent == displayMostRecent.moviesWatched)
                {
                  if (visibleOn == null)
                    visibleOn = "[control.hasfocus(" + (menItem.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                  else
                    visibleOn += "|control.hasfocus(" + (menItem.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                }
              }
            }
          }
        }
      }
      //
      //Controls to display recent TVSeries overlay
      //
      if (isOverlay == isOverlayType.TVSeries)
      {       
        foreach (menuItem item in menuItems)
        {
          if (item.showMostRecent == displayMostRecent.tvSeriesAdded)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + item.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + item.id.ToString() + ")";
          }
          if (item.showMostRecent == displayMostRecent.tvseriesWatched)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + item.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + item.id.ToString() + ")";
          }
          // Check Sunmenu Level 1
          if (item.subMenuLevel1.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel1.Count; i++)
            {
              if (item.subMenuLevel1[i].showMostRecent == displayMostRecent.tvseriesAddedWatched)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
              }
              else
              {
                if (item.subMenuLevel1[i].showMostRecent == displayMostRecent.tvSeriesAdded)
                {
                  if (visibleOn == null)
                    visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                  else
                    visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                }
                if (item.subMenuLevel1[i].showMostRecent == displayMostRecent.tvseriesWatched)
                {
                  if (visibleOn == null)
                    visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                  else
                    visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                }
              }
            }
          }
          // Check Sunmenu Level 2
          if (item.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel2.Count; i++)
            {
              if (item.subMenuLevel2[i].showMostRecent == displayMostRecent.tvseriesAddedWatched)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
              }
              else
              {
                if (item.subMenuLevel2[i].showMostRecent == displayMostRecent.tvSeriesAdded)
                {
                  if (visibleOn == null)
                    visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                  else
                    visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                }
                if (item.subMenuLevel2[i].showMostRecent == displayMostRecent.tvseriesWatched)
                {
                  if (visibleOn == null)
                    visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                  else
                    visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                }
              }
            }

          }
        }
      }
      if (visibleOn != null)
        visibleOn += "]";
      return visibleOn;
    }

    #endregion
  }
}
