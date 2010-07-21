namespace StreamedMPEditor
{
  public partial class streamedMpEditor
  {

    #region Main

    void generateMostRecentOverlay(chosenMenuStyle menuStyle, isOverlayType isOverlay)
    {
      if (isOverlay == isOverlayType.TVSeries)
      {
        doTVSeriesV(tvSeriesRecentStyle);
      }

    }
    //
    // Summary or Full, vertical or horizonal
    //
    void doTVSeriesV(tvSeriesRecentType reqType)
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
          horizontalTVSeriesSummary();
        }
        else
        {
          horizonalTVSeriesFull();
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
                  "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series1.thumb,#)</visible>\n" +
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
                    "<label>#infoservice.recentlyAdded.series1.title</label>\n" +
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
                    "<label>#infoservice.recentlyAdded.series1.episodetitle</label>\n" +
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
                    "<label>#TVSeries.Translation.Season.Label: #infoservice.recentlyAdded.series1.season #TVSeries.Translation.Episode.Label: #infoservice.recentlyAdded.series1.episodenumber</label>\n" +
                    "<font>mediastream10</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                "</control>\n" +
                "<control>\n" +
                  "<description>GROUP: RecentlyAdded Series</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series2.thumb,#)</visible>\n" +
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
                    "<description>Series 2 Rounded Cover</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>892</posX>\n" +
                    "<posY>270</posY>\n" +
                    "<height>181</height>\n" +
                    "<width>129</width>\n" +
                    "<texture>round.poster.frame.noreflection.png</texture>\n" +
                  "</control>      \n" +
                  "<control>\n" +
                    "<description>Series 2 name</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>299</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#infoservice.recentlyAdded.series2.title</label>\n" +
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
                    "<label>#infoservice.recentlyAdded.series2.episodetitle</label>\n" +
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
                    "<label>#TVSeries.Translation.Season.Label: #infoservice.recentlyAdded.series2.season #TVSeries.Translation.Episode.Label: #infoservice.recentlyAdded.series2.episodenumber</label>\n" +
                    "<font>mediastream10</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                "</control>\n" +
                "<control>\n" +
                  "<description>GROUP: RecentlyAdded Series</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series3.thumb,#)</visible>\n" +
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
                    "<description>Series 3 Rounded Cover</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>892</posX>\n" +
                    "<posY>470</posY>\n" +
                    "<height>181</height>\n" +
                    "<width>129</width>\n" +
                    "<texture>round.poster.frame.noreflection.png</texture>\n" +
                  "</control>      \n" +
                  "<control>\n" +
                    "<description>Series 3 name</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>498</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#infoservice.recentlyAdded.series3.title</label>\n" +
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
                    "<label>#infoservice.recentlyAdded.series3.episodetitle</label>\n" +
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
                    "<label>#TVSeries.Translation.Season.Label: #infoservice.recentlyAdded.series3.season #TVSeries.Translation.Episode.Label: #infoservice.recentlyAdded.series3.episodenumber</label>\n" +
                    "<font>mediastream10</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                "</control>\n" +
                "<control>\n" +
                    "<description>Series 1 thumb</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>895</posX>\n" +
                    "<posY>79</posY>\n" +
                    "<height>165</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>#infoservice.recentlyAdded.series1.thumb</texture>\n" +
                    "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series1.thumb,#)+string.equals(#StreamedMP.FullHD,false)</visible>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 thumb (1920x1080)</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1351</posX>\n" +
                    "<posY>118</posY>\n" +
                    "<height>249</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>#infoservice.recentlyAdded.series1.thumb</texture>\n" +
                    "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series1.thumb,#)+string.equals(#StreamedMP.FullHD,true)</visible>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 thumb</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>895</posX>\n" +
                    "<posY>276</posY>\n" +
                    "<height>163</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>#infoservice.recentlyAdded.series2.thumb</texture>\n" +
                    "<visible>string.equals(#StreamedMP.FullHD,false)</visible>\n" +
                    "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series2.thumb,#)+string.equals(#StreamedMP.FullHD,false)</visible>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 thumb (1920x1080)</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1351</posX>\n" +
                    "<posY>417</posY>\n" +
                    "<height>249</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>#infoservice.recentlyAdded.series2.thumb</texture>\n" +
                    "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series2.thumb,#)+string.equals(#StreamedMP.FullHD,true)</visible>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 thumb</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>895</posX>\n" +
                    "<posY>475</posY>\n" +
                    "<height>165</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>#infoservice.recentlyAdded.series3.thumb</texture>\n" +
                    "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series3.thumb,#)+string.equals(#StreamedMP.FullHD,false)</visible>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 thumb (1920x1080)</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1351</posX>\n" +
                    "<posY>717</posY>\n" +
                    "<height>249</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>#infoservice.recentlyAdded.series3.thumb</texture>\n" +
                    "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series3.thumb,#)+string.equals(#StreamedMP.FullHD,true)</visible>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "</control>\n" +
              "</controls>\n" +
            "</window>";

      writeXMLFile("basichome.recentlyadded.tvseries.VFull.xml");

    }

    #endregion

    #region TVSeries Vertical Summary

    void verticalTVSeriesSummary()
    {
      if (mostRecentStyle == mostRecentSummaryStyle.poster)
      {
        buildSummaryFile(475, mostRecentSummaryStyle.poster);
        writeXMLFile("basichome.recentlyadded.tvseries.VSum.xml");
      }
      if (mostRecentStyle == mostRecentSummaryStyle.fanart)
      {
        buildSummaryFile(475, mostRecentSummaryStyle.fanart);
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
        hdOffset = 276;
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
                    "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series1.thumb,#)</visible>" +
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
                      "<label>#infoservice.recentlyAdded.series1.title</label>" +
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
                      "<label>#infoservice.recentlyAdded.series1.episodetitle</label>" +
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
                      "<label>#TVSeries.Translation.Season.Label: #infoservice.recentlyAdded.series1.season #TVSeries.Translation.Episode.Label: #infoservice.recentlyAdded.series1.episodenumber</label>" +
                      "<font>mediastream10</font>" +
                      "<textcolor>White</textcolor>" +
                    "</control>" +
                  "</control>" +
                  "<control>" +
                    "<description>GROUP: RecentlyAdded Series</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series2.thumb,#)</visible>" +
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
                      "<description>Series 2 Rounded Cover</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>441</posX>" +
                      "<posY>" + (overlayYpos + 2).ToString() + "</posY>" +
                      "<height>181</height>" +
                      "<width>129</width>" +
                      "<texture>round.poster.frame.noreflection.png</texture>" +
                    "</control>      " +
                    "<control>" +
                      "<description>Series 2 name</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>568</posX>" +
                      "<posY>" + (overlayYpos + 29).ToString() + "</posY>" +
                      "<width>258</width>" +
                      "<label>#infoservice.recentlyAdded.series2.title</label>" +
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
                      "<label>#infoservice.recentlyAdded.series2.episodetitle</label>" +
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
                      "<label>#TVSeries.Translation.Season.Label: #infoservice.recentlyAdded.series2.season #TVSeries.Translation.Episode.Label: #infoservice.recentlyAdded.series2.episodenumber</label>" +
                      "<font>mediastream10</font>" +
                      "<textcolor>White</textcolor>" +
                    "</control>" +
                  "</control>" +
                  "<control>" +
                    "<description>GROUP: RecentlyAdded Series</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series3.thumb,#)</visible>" +
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
                      "<description>Series 3 Rounded Cover</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>855</posX>" +
                      "<posY>" + (overlayYpos + 2).ToString() + "</posY>" +
                      "<height>181</height>" +
                      "<width>129</width>" +
                      "<texture>round.poster.frame.noreflection.png</texture>" +
                    "</control>      " +
                    "<control>" +
                      "<description>Series 3 name</description>" +
                      "<type>label</type>" +
                      "<id>0</id>" +
                      "<posX>981</posX>" +
                      "<posY>" + (overlayYpos + 29).ToString() + "</posY>" +
                      "<width>255</width>" +
                      "<label>#infoservice.recentlyAdded.series3.title</label>" +
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
                      "<label>#infoservice.recentlyAdded.series3.episodetitle</label>" +
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
                      "<label>#TVSeries.Translation.Season.Label: #infoservice.recentlyAdded.series3.season #TVSeries.Translation.Episode.Label: #infoservice.recentlyAdded.series3.episodenumber</label>" +
                      "<font>mediastream10</font>" +
                      "<textcolor>White</textcolor>" +
                    "</control>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 1 thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>33</posX>" +
                    "<posY>" + (overlayYpos + 7).ToString() + "</posY>" +
                    "<height>165</height>" +
                    "<keepaspectratio>true</keepaspectratio>" +
                    "<texture>#infoservice.recentlyAdded.series1.thumb</texture>" +
                    "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series1.thumb,#)+string.equals(#StreamedMP.FullHD,false)</visible>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,300" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 1 thumb (1920x1080)</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>52</posX>" +
                    "<posY>" + (overlayYpos + 7 + hdOffset).ToString() + "</posY>" +
                    "<height>249</height>" +
                    "<keepaspectratio>true</keepaspectratio>" +
                    "<texture>#infoservice.recentlyAdded.series1.thumb</texture>" +
                    "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series1.thumb,#)+string.equals(#StreamedMP.FullHD,true)</visible>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,300" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 2 thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>447</posX>" +
                    "<posY>" + (overlayYpos + 7).ToString() + "</posY>" +
                    "<height>165</height>" +
                    "<keepaspectratio>true</keepaspectratio>" +
                    "<texture>#infoservice.recentlyAdded.series2.thumb</texture>" +
                    "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series2.thumb,#)+string.equals(#StreamedMP.FullHD,false)</visible>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,300" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 2 thumb (1920x1080)</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>675</posX>" +
                    "<posY>" + (overlayYpos + 7 + hdOffset).ToString() + "</posY>" +
                    "<height>249</height>" +
                    "<keepaspectratio>true</keepaspectratio>" +
                    "<texture>#infoservice.recentlyAdded.series2.thumb</texture>" +
                    "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series2.thumb,#)+string.equals(#StreamedMP.FullHD,true)</visible>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,300" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 3 thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>859</posX>" +
                    "<posY>" + (overlayYpos + 7).ToString() + "</posY>" +
                    "<height>165</height>" +
                    "<keepaspectratio>true</keepaspectratio>" +
                    "<texture>#infoservice.recentlyAdded.series3.thumb</texture>" +
                    "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series3.thumb,#)+string.equals(#StreamedMP.FullHD,false)</visible>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,300" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 3 thumb (1920x1080)</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>1295</posX>" +
                    "<posY>" + (overlayYpos + 7 + hdOffset).ToString() + "</posY>" +
                    "<height>249</height>" +
                    "<keepaspectratio>true</keepaspectratio>" +
                    "<texture>#infoservice.recentlyAdded.series3.thumb</texture>" +
                    "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series3.thumb,#)+string.equals(#StreamedMP.FullHD,true)</visible>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,300" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
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
      if (mostRecentStyle == mostRecentSummaryStyle.poster)
      {
        buildSummaryFile(overlayYpos, mostRecentSummaryStyle.poster);
        writeXMLFile("basichome.recentlyadded.tvseries.HSum.xml");
      }
      if (mostRecentStyle == mostRecentSummaryStyle.fanart)
      {
        buildSummaryFile(overlayYpos, mostRecentSummaryStyle.fanart);
        writeXMLFile("basichome.recentlyadded.tvseries.HSum2.xml");
      }


    }

    #endregion

    #region Summary Builder

    void buildSummaryFile(int overlayYpos, mostRecentSummaryStyle sumStyle)
    {
      #region Summary Style 1

      if (sumStyle == mostRecentSummaryStyle.poster)
      {
        // Build the file
        xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>" +
              "<window>" +
                "<controls>" +
                  "<control>" +
                    "<description>GROUP: RecentlyAdded Series 3</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series3.thumb,#)</visible>" +
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
                      "<texture>#infoservice.recentlyAdded.series3.thumb</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 3 episode</description>" +
                      "<type>label</type>" +
                      "<id>0</id>" +
                      "<posX>871</posX>" +
                      "<posY>" + (overlayYpos + 194).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<label>#infoservice.recentlyAdded.series3.seasonx#infoservice.recentlyAdded.series3.episodenumber</label>" +
                      "<font>mediastream10tc</font>" +
                      "<align>center</align>" +
                      "<textcolor>White</textcolor>" +
                    "</control>" +
                  "</control>" +
                  "<control>" +
                    "<description>GROUP: RecentlyAdded Series 2</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series2.thumb,#)</visible>" +
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
                      "<texture>#infoservice.recentlyAdded.series2.thumb</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 2 episode</description>" +
                      "<type>label</type>" +
                      "<id>0</id>" +
                      "<posX>1011</posX>" +
                      "<posY>" + (overlayYpos + 194).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<label>#infoservice.recentlyAdded.series2.seasonx#infoservice.recentlyAdded.series2.episodenumber</label>" +
                      "<font>mediastream10tc</font>" +
                      "<align>center</align>" +
                      "<textcolor>White</textcolor>" +
                    "</control>" +
                  "</control>" +
                  "<control>" +
                    "<description>GROUP: RecentlyAdded Series 1</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series1.thumb,#)</visible>" +
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
                      "<texture>#infoservice.recentlyAdded.series1.thumb</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 3 episode</description>" +
                      "<type>label</type>" +
                      "<id>0</id>" +
                      "<posX>1151</posX>" +
                      "<posY>" + (overlayYpos + 194).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<label>#infoservice.recentlyAdded.series1.seasonx#infoservice.recentlyAdded.series1.episodenumber</label>" +
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

      if (sumStyle == mostRecentSummaryStyle.fanart)
      {
        string fanartProperty = "#infoservice.recentlyAdded.series1.fanart";

        if (cbCycleFanart.Checked)
          fanartProperty = "#StreamedMP.MostRecentImageFanart";

        xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>\n" +
              "<window>\n" +
                "<controls>\n" +
                  "<control>\n" +
                    "<description>GROUP: RecentlyAdded Series</description>\n" +
                    "<type>group</type>\n" +
                    "<dimColor>0xffffffff</dimColor>\n" +
                    "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series1.fanart,#)</visible>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "<control>\n" +
                    "<description>Series 1 BG</description>\n" +
                    "<posX>976</posX>\n" +
                    "<posY>50</posY>\n" +
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
                    "<posX>995</posX>\n" +
                    "<posY>70</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>LATEST EPISODES</label>\n" +
                    "<font>mediastream10tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>      " +
                  "<control>\n" +
                    "<description>Series 1 name</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>995</posX>\n" +
                    "<posY>245</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#infoservice.recentlyAdded.series1.title - #infoservice.recentlyAdded.series1.seasonx#infoservice.recentlyAdded.series1.episodenumber</label>\n" +
                    "<font>mediastream9c</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 title</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>995</posX>\n" +
                    "<posY>260</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#infoservice.recentlyAdded.series1.episodetitle</label>\n" +
                    "<font>mediastream10tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>" +
                "</control>\n" +
                "<control>\n" +
                  "<description>GROUP: RecentlyAdded Series</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series2.fanart,#)</visible>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "<control>\n" +
                    "<description>Series 2 name</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>995</posX>\n" +
                    "<posY>280</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#infoservice.recentlyAdded.series2.title - #infoservice.recentlyAdded.series2.seasonx#infoservice.recentlyAdded.series2.episodenumber</label>\n" +
                    "<font>mediastream9c</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 title</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>995</posX>\n" +
                    "<posY>295</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#infoservice.recentlyAdded.series2.episodetitle</label>\n" +
                    "<font>mediastream10tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                "</control>\n" +
                "<control>\n" +
                  "<description>GROUP: RecentlyAdded Series</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series3.fanart,#)</visible>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "<control>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>995</posX>\n" +
                    "<posY>315</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#infoservice.recentlyAdded.series3.title - #infoservice.recentlyAdded.series3.seasonx#infoservice.recentlyAdded.series3.episodenumber</label>\n" +
                    "<font>mediastream9c</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 title</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>995</posX>\n" +
                    "<posY>330</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#infoservice.recentlyAdded.series3.episodetitle</label>\n" +
                    "<font>mediastream10tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>/n" +
                "</control>/n" +
                "<control>\n" +
                  "<description>Series 1 thumb/fanart</description>\n" +
                  "<type>image</type>\n" +
                  "<id>0</id>\n" +
                  "<posX>990</posX>\n" +
                  "<posY>92</posY>\n" +
                  "<width>265</width>\n" +
                  "<keepaspectratio>true</keepaspectratio>\n" +
                  "<texture>" + fanartProperty + "</texture>\n" +
                  "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series1.fanart,#)+string.equals(#StreamedMP.FullHD,false)</visible>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                "</control>\n" +
                "<control>\n" +
                  "<description>Series 1 thumb/fanart (1920x1080)</description>\n" +
                  "<type>image</type>\n" +
                  "<id>0</id>\n" +
                  "<posX>1495</posX>\n" +
                  "<posY>137</posY>\n" +
                  "<width>400</width>\n" +
                  "<keepaspectratio>true</keepaspectratio>\n" +
                  "<texture>" + fanartProperty + "</texture>\n" +
                  "<visible>control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ") + !string.starts(#infoservice.recentlyAdded.series1.fanart,#)+string.equals(#StreamedMP.FullHD,true)</visible>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                "</control>\n" +                
              "</controls>\n" +
            "</window>";
      }

      #endregion
    }
    #endregion

  }
}
