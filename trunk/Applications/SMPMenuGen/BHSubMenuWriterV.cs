using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Linq;

namespace SMPMenuGen
{
  public partial class GenerateMenu
  {
    public string localxml = string.Empty;
    string onup;
    string ondown;
    string subArrowVisible;

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
                      "<posX>" + (int.Parse(txtMenuPos.Text) - 5).ToString() + "</posX>" +
                      "<posY>325</posY>" +
                      "<width>225</width>" +
                      "<height>405</height>" +
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
                "<label>" + parentMenu.subMenuLevel1[j].displayName + "</label>" +
                "<width>225</width>";

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
              localxml += "<hyperlinkParameter>site:" + parentMenu.subMenuLevel1[j].hyperlinkParameter + "|return:" + parentMenu.subMenuLevel1[j].hyperlinkParameterOption + "</hyperlinkParameter>";
              break;
            default:
              localxml += "<hyperlinkParameter>" + parentMenu.subMenuLevel1[j].hyperlinkParameter + "</hyperlinkParameter>";
              break;
          }
        }

        localxml += "<onleft>" + (parentMenu.id + 900).ToString() + "</onleft>" +
                     "<onright>" + (parentMenu.subMenuLevel1ID + (1 + isSecondLevel)).ToString() + "</onright>" +
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
                  "<posX>" + (int.Parse(txtMenuPos.Text) - 27).ToString() + "</posX>" +
                  "<posY>330</posY>" +
                  "<align>right</align>" +
                  "<width>16</width>" +
                  "<height>16</height>" +
                  "<visible>" + subArrowVisible + "</visible>" +
                  "<texture>arrowrightfo.png</texture>" +
                  "<colordiffuse>77ffffff</colordiffuse>" +
                  "<animation effect=\"fade\" time=\"400\">Visible</animation>" +
                  "<animation effect=\"fade\" time=\"400\" delay=\"400\">WindowOpen</animation>" +
                  "<animation effect=\"slide\" end=\"-400,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowClose</animation>" +
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
                  "<posX>" + (int.Parse(txtMenuPos.Text) + 195).ToString() + "</posX>" +
                  "<posY>338</posY>" +
                  "<align>right</align>" +
                  "<width>16</width>" +
                  "<height>16</height>" +
                  "<visible>" + subArrowVisible + "</visible>" +
                  "<texture>arrowrightfo.png</texture>" +
                  "<colordiffuse>77ffffff</colordiffuse>" +
                  "<animation effect=\"fade\" time=\"400\">Visible</animation>" +
                  "<animation effect=\"fade\" time=\"400\" delay=\"400\">WindowOpen</animation>" +
                  "<animation effect=\"slide\" end=\"-400,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowClose</animation>" +
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
                      "<posX>" + (int.Parse(txtMenuPos.Text) + 226).ToString() + "</posX>" +
                      "<posY>325</posY>" +
                      "<width>225</width>" +
                      "<height>405</height>" +
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
                "<label>" + parentMenu.subMenuLevel2[j].displayName + "</label>" +
                "<width>225</width>";

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
              localxml += "<hyperlinkParameter>site:" + parentMenu.subMenuLevel2[j].hyperlinkParameter + "|return:" + parentMenu.subMenuLevel2[j].hyperlinkParameterOption + "</hyperlinkParameter>";
              break;
            default:
              localxml += "<hyperlinkParameter>" + parentMenu.subMenuLevel2[j].hyperlinkParameter + "</hyperlinkParameter>";
              break;
          }
        }

        localxml += "<onleft>" + (parentMenu.subMenuLevel1ID + (1)).ToString() + "</onleft>" +
                     "<onright>" + (parentMenu.subMenuLevel1ID + (j + 101)).ToString() + "</onright>" +
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
