﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Linq;
using System.Web;

namespace StreamedMPEditor
{
  public partial class formStreamedMpEditor
  {
    public string localxml = string.Empty;
    string onup;
    string ondown;
    string subArrowVisible;
    string rightArrowValue;

    string bhSubMenuWriterV()
    {
      level1LateralBladeVisible = "control.isvisible(";
      level2LateralBladeVisible = "control.isvisible(";
      foreach (menuItem menItem in menuItems)
      {
        if (menItem.subMenuLevel1.Count > 0)
        {
          level1LateralBladeVisible += menItem.subMenuLevel1ID.ToString() + ")|control.isvisible(";

          if (menItem.subMenuLevel2.Count > 0)
            level1LateralBladeVisible += (menItem.subMenuLevel1ID + 100).ToString() + ")|control.isvisible(";

          writeSubMenuLevel1V(menItem);
        }
        if (menItem.subMenuLevel2.Count > 0)
        {
          level2LateralBladeVisible += (menItem.subMenuLevel1ID + 100).ToString() + ")|control.isvisible(";
          writeSubMenuLevel2V(menItem);
        }
      }
      localxml += "<!--             End of Submenu Code            -->";
      return localxml;
    }

    string writeSubMenuLevel1V(formStreamedMpEditor.menuItem parentMenu)
    {
      string dummyFocusControls = "Control.HasFocus(";

      int isSecondLevel = 0;
      if (parentMenu.subMenuLevel2.Count > 0)
        isSecondLevel = 100;

      for (int i = 0; i < parentMenu.subMenuLevel1.Count; i++)
      {
        if (i < (parentMenu.subMenuLevel1.Count - 1))
          dummyFocusControls += (parentMenu.subMenuLevel1ID + (i + 1)).ToString() + ")|Control.HasFocus(";
        else
          if (parentMenu.subMenuLevel2.Count == 0)
            dummyFocusControls += (parentMenu.subMenuLevel1ID + (i + 1)).ToString() + ")";
          else
            dummyFocusControls += (parentMenu.subMenuLevel1ID + (i + 1)).ToString() + ")|Control.HasFocus(";
      }
      if (parentMenu.subMenuLevel2.Count > 0)
      {
        for (int i = 0; i < parentMenu.subMenuLevel2.Count; i++)
        {
          if (i < (parentMenu.subMenuLevel2.Count - 1))
            dummyFocusControls += (parentMenu.subMenuLevel1ID + (i + 101)).ToString() + ")|Control.HasFocus(";
          else
            dummyFocusControls += (parentMenu.subMenuLevel1ID + (i + 101)).ToString() + ")";
        }
      }

      localxml += "<control>" +
                      "<description>dummy for lateral blade visibility</description>" +
                      "<type>label</type>" +
                      "<id>" + parentMenu.subMenuLevel1ID.ToString() + "</id>" +
                      "<label></label>" +
                      "<visible>" + dummyFocusControls + "</visible>" +
                    "</control>" +
                    "<control>" +
                      "<type>group</type>" +
                      "<description>group element</description>" +
                      "<animation effect=\"fade\" time=\"200\" delay=\"400\">WindowOpen</animation>" +
                      "<animation effect=\"fade\" time=\"200\">WindowClose</animation>" +
                      "<animation effect=\"fade\" time=\"200\">visible</animation>" +
                      "<animation effect=\"fade\" time=\"200\">hidden</animation>" +
                      "<posX>" + (int.Parse(txtMenuPos.Text) - 8).ToString() + "</posX>" +
                      "<posY>488</posY>" +
                      "<width>338</width>" +
                      "<height>608</height>" +
                      "<dimColor>ffffffff</dimColor>" +
                      "<layout>StackLayout</layout>";

      for (int j = 0; j < parentMenu.subMenuLevel1.Count; j++)
      {
        onup = (parentMenu.subMenuLevel1ID + (j)).ToString();
        ondown = (parentMenu.subMenuLevel1ID + (j + 2)).ToString();
        if (j == 0)
        {
          onup = (parentMenu.subMenuLevel1ID + parentMenu.subMenuLevel1.Count).ToString();
          ondown = (parentMenu.subMenuLevel1ID + (j + 2)).ToString();
        }

        if (j == (parentMenu.subMenuLevel1.Count - 1))
        {
          onup = (parentMenu.subMenuLevel1ID + j).ToString();
          ondown = (parentMenu.subMenuLevel1ID + 1).ToString();
        }

        localxml += "<control Style=\"settingsbutton\">" +
                "<description>SUB ITEM " + j.ToString() + "</description>" +
                "<type>button</type>" +
                "<id>" + (parentMenu.subMenuLevel1ID + (j + 1)).ToString() + "</id>" +
                "<label>" + HttpUtility.HtmlEncode(parentMenu.subMenuLevel1[j].displayName) + "</label>" +
                "<width>338</width>";

        if (parentMenu.subMenuLevel1[j].hyperlink == "196299")
        localxml += "<action>99</action>";
       else if (parentMenu.subMenuLevel1[j].hyperlink == "196297")
        localxml += "<action>97</action>";
       else if (parentMenu.subMenuLevel1[j].hyperlink == "196250")
         localxml += "<action>196250</action>";
       else
          if (parentMenu.subMenuLevel1[j].hyperlink == formStreamedMpEditor.musicSkinID && parentMenu.subMenuLevel1[j].hyperlinkParameter != "false")
            localxml += "<hyperlink>504</hyperlink>";
          else
            localxml += "<hyperlink>" + parentMenu.subMenuLevel1[j].hyperlink + "</hyperlink>";

        //Plugin Parameter handling
        if (parentMenu.subMenuLevel1[j].hyperlinkParameter != "false")
        {
          switch (parentMenu.subMenuLevel1[j].hyperlink)
          {
            case onlineVideosSkinID:
              if (!IsOnlineVideosGroup(parentMenu.subMenuLevel1[j].hyperlinkParameter))
              {
                string search = string.IsNullOrEmpty(parentMenu.subMenuLevel1[j].hyperlinkParameterSearch) ? string.Empty : "|search:" + parentMenu.subMenuLevel1[j].hyperlinkParameterSearch;
                string category = string.IsNullOrEmpty(parentMenu.subMenuLevel1[j].hyperlinkParameterCategory) ? string.Empty : "|category:" + parentMenu.subMenuLevel1[j].hyperlinkParameterCategory;
                localxml += "<hyperlinkParameter>site:" + HttpUtility.HtmlEncode(parentMenu.subMenuLevel1[j].hyperlinkParameter + category + search) + "|return:" + parentMenu.subMenuLevel1[j].hyperlinkParameterOption + "</hyperlinkParameter>";
              }
              else
              {
                localxml += "<hyperlinkParameter>group:" + HttpUtility.HtmlEncode(parentMenu.subMenuLevel1[j].hyperlinkParameter.Substring(7)) + "|return:" + parentMenu.subMenuLevel1[j].hyperlinkParameterOption + "</hyperlinkParameter>";
              }
              break;
            case movingPicturesSkinID:
              localxml += "<hyperlinkParameter>categoryid:" + HttpUtility.HtmlEncode(parentMenu.subMenuLevel1[j].hyperlinkParameter) + "</hyperlinkParameter>";
              break;
            default:
              localxml += "<hyperlinkParameter>" + HttpUtility.HtmlEncode(parentMenu.subMenuLevel1[j].hyperlinkParameter) + "</hyperlinkParameter>";
              break;
          }
        }

        // Right arrow to 2nd level menu or exit/shutdown/restart.. menu
        if (cbExitStyleNew.Checked || parentMenu.subMenuLevel2.Count > 0)
          rightArrowValue = (parentMenu.subMenuLevel1ID + (1 + isSecondLevel)).ToString();
        else
          rightArrowValue = "7777";

       localxml += "<onleft>" + (parentMenu.id + 900).ToString() + "</onleft>" +
                    "<onright>" + rightArrowValue + "</onright>" +
                    "<ondown>" + ondown + "</ondown>" +
                    "<onup>" + onup + "</onup>" +
                    "<visible allowhiddenfocus=\"true\">control.isvisible(" + parentMenu.subMenuLevel1ID.ToString() + ")</visible>" +
                  "</control>";
      }
      localxml += "</control>";

      // main level indocation arrow
      subArrowVisible = "control.isvisible(11111)|Control.HasFocus(";
      foreach (menuItem item in menuItems)
      {
        if (item.subMenuLevel1ID != 0)
          subArrowVisible += (item.id + 700).ToString() + ")|control.hasfocus(" + (item.id + 800).ToString() + ")|control.hasfocus(" + (item.id + 900).ToString() + ")|control.hasfocus(";
      }
      subArrowVisible = subArrowVisible.Substring(0, (subArrowVisible.Length - 18));

      localxml += "<control>" +
                  "<description>Sub Menu Indicator (Main)</description>" +
                  "<type>image</type>" +
                  "<posX>" + (int.Parse(txtMenuPos.Text) - 41).ToString() + "</posX>" +
                  "<posY>495</posY>" +
                  "<align>right</align>" +
                  "<width>24</width>" +
                  "<height>24</height>" +
                  "<visible>" + subArrowVisible + "</visible>" +
                  "<texture>arrowrightfo.png</texture>" +
                  "<colordiffuse>77ffffff</colordiffuse>" +
                  "<animation effect=\"fade\" time=\"400\">Visible</animation>" +
                  "<animation effect=\"fade\" time=\"400\" delay=\"400\">WindowOpen</animation>" +
                  "<animation effect=\"slide\" end=\"-600,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowClose</animation>" +
                  "<animation effect=\"fade\" time=\"400\">WindowClose</animation>" +
              "</control>";

      // 2nd level indicator arrow
      subArrowVisible = "control.isvisible(22222)|control.hasfocus(";
      foreach (menuItem item in menuItems)
      {
        if (item.subMenuLevel1ID != 0)
          if (item.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel1.Count; i++)
            {
              subArrowVisible += (item.subMenuLevel1ID + (i + 1)).ToString() + ")|control.hasfocus(";
            }
          }
      }
      subArrowVisible = subArrowVisible.Substring(0, (subArrowVisible.Length - 18));
      localxml += "<control>" +
                  "<description>Sub Menu Indicator (Level1)</description>" +
                  "<type>image</type>" +
                  "<posX>" + (int.Parse(txtMenuPos.Text) + 293).ToString() + "</posX>" +
                  "<posY>507</posY>" +
                  "<align>right</align>" +
                  "<width>24</width>" +
                  "<height>24</height>" +
                  "<visible>" + subArrowVisible + "</visible>" +
                  "<texture>arrowrightfo.png</texture>" +
                  "<colordiffuse>77ffffff</colordiffuse>" +
                  "<animation effect=\"fade\" time=\"400\">Visible</animation>" +
                  "<animation effect=\"fade\" time=\"400\" delay=\"400\">WindowOpen</animation>" +
                  "<animation effect=\"slide\" end=\"-600,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowClose</animation>" +
                  "<animation effect=\"fade\" time=\"400\">WindowClose</animation>" +
                "</control>";
      return localxml;
    }

    string writeSubMenuLevel2V(formStreamedMpEditor.menuItem parentMenu)
    {
      string dummyFocusControls = "Control.HasFocus(";
      for (int i = 0; i < parentMenu.subMenuLevel2.Count; i++)
      {
        if (i < (parentMenu.subMenuLevel2.Count - 1))
          dummyFocusControls += (parentMenu.subMenuLevel1ID + (i + 101)).ToString() + ")|Control.HasFocus(";
        else
          dummyFocusControls += (parentMenu.subMenuLevel1ID + (i + 101)).ToString() + ")";
      }

      localxml += "<control>" +
                      "<description>dummy for lateral blade visibility</description>" +
                      "<type>label</type>" +
                      "<id>" + (parentMenu.subMenuLevel1ID + 100).ToString() + "</id>" +
                      "<label></label>" +
                      "<visible>" + dummyFocusControls + "</visible>" +
                    "</control>" +
                    "<control>" +
                      "<type>group</type>" +
                      "<description>group element</description>" +
                      "<animation effect=\"fade\" time=\"200\" delay=\"400\">WindowOpen</animation>" +
                      "<animation effect=\"fade\" time=\"200\">WindowClose</animation>" +
                      "<animation effect=\"fade\" time=\"200\">visible</animation>" +
                      "<animation effect=\"fade\" time=\"200\">hidden</animation>" +
                      "<posX>" + (int.Parse(txtMenuPos.Text) + 335).ToString() + "</posX>" +
                      "<posY>488</posY>" +
                      "<width>338</width>" +
                      "<height>608</height>" +
                      "<dimColor>ffffffff</dimColor>" +
                      "<layout>StackLayout</layout>";

      for (int j = 0; j < parentMenu.subMenuLevel2.Count; j++)
      {
        onup = (parentMenu.subMenuLevel1ID + (j + 100)).ToString();
        ondown = (parentMenu.subMenuLevel1ID + (j + 102)).ToString();
        if (j == 0)
        {
          onup = (parentMenu.subMenuLevel1ID + parentMenu.subMenuLevel2.Count).ToString();
          ondown = (parentMenu.subMenuLevel1ID + (j + 102)).ToString();
        }

        if (j == (parentMenu.subMenuLevel2.Count - 1))
        {
          onup = (parentMenu.subMenuLevel1ID + 100 + j).ToString();
          ondown = (parentMenu.subMenuLevel1ID + 101).ToString();
        }

        localxml += "<control Style=\"settingsbutton\">" +
                "<description>SUB ITEM " + j.ToString() + "</description>" +
                "<type>button</type>" +
                "<id>" + (parentMenu.subMenuLevel1ID + (j + 101)).ToString() + "</id>" +
                "<label>" + HttpUtility.HtmlEncode(parentMenu.subMenuLevel2[j].displayName) + "</label>" +
                "<width>338</width>";

        if (parentMenu.subMenuLevel2[j].hyperlink == "196299")
          localxml += "<action>99</action>";
        else if (parentMenu.subMenuLevel2[j].hyperlink == "196297")
          localxml += "<action>97</action>";
        else if (parentMenu.subMenuLevel2[j].hyperlink == "196250")
          localxml += "<action>196250</action>";
        else
          if (parentMenu.subMenuLevel2[j].hyperlink == formStreamedMpEditor.musicSkinID && parentMenu.subMenuLevel2[j].hyperlinkParameter != "false")
            localxml += "<hyperlink>504</hyperlink>";
          else
            localxml += "<hyperlink>" + parentMenu.subMenuLevel2[j].hyperlink + "</hyperlink>";

        //Plugin Parameter handling
        if (parentMenu.subMenuLevel2[j].hyperlinkParameter != "false")
        {
          switch (parentMenu.subMenuLevel2[j].hyperlink)
          {
            case onlineVideosSkinID:
              if (!IsOnlineVideosGroup(parentMenu.subMenuLevel2[j].hyperlinkParameter))
              {
                string search = string.IsNullOrEmpty(parentMenu.subMenuLevel2[j].hyperlinkParameterSearch) ? string.Empty : "|search:" + parentMenu.subMenuLevel2[j].hyperlinkParameterSearch;
                string category = string.IsNullOrEmpty(parentMenu.subMenuLevel2[j].hyperlinkParameterCategory) ? string.Empty : "|category:" + parentMenu.subMenuLevel2[j].hyperlinkParameterCategory;
                localxml += "<hyperlinkParameter>site:" + HttpUtility.HtmlEncode(parentMenu.subMenuLevel2[j].hyperlinkParameter + category + search) + "|return:" + parentMenu.subMenuLevel2[j].hyperlinkParameterOption + "</hyperlinkParameter>";
              }
              else
              {
                localxml += "<hyperlinkParameter>group:" + HttpUtility.HtmlEncode(parentMenu.subMenuLevel2[j].hyperlinkParameter.Substring(7)) + "|return:" + parentMenu.subMenuLevel2[j].hyperlinkParameterOption + "</hyperlinkParameter>";
              }
              break;
            case movingPicturesSkinID:
              localxml += "<hyperlinkParameter>categoryid:" + HttpUtility.HtmlEncode(parentMenu.subMenuLevel2[j].hyperlinkParameter) + "</hyperlinkParameter>";
              break;
            default:
              localxml += "<hyperlinkParameter>" + HttpUtility.HtmlEncode(parentMenu.subMenuLevel2[j].hyperlinkParameter) + "</hyperlinkParameter>";
              break;
          }
        }

        if (cbExitStyleNew.Checked)
          rightArrowValue = (parentMenu.subMenuLevel1ID + (j + 101)).ToString();
        else
          rightArrowValue = "7777";

        localxml += "<onleft>" + (parentMenu.subMenuLevel1ID + (1)).ToString() + "</onleft>" +
                     "<onright>" + rightArrowValue + "</onright>" +
                     "<ondown>" + ondown + "</ondown>" +
                     "<onup>" + onup + "</onup>" +
                     "<visible allowhiddenfocus=\"true\">control.isvisible(" + (parentMenu.subMenuLevel1ID + 100).ToString() + ")</visible>" +
                   "</control>";
      }
      localxml += "</control>";
      return localxml;
    }
  }
}
