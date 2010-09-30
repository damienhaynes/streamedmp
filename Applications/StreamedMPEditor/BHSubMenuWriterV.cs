using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Linq;

namespace StreamedMPEditor
{
  public partial class formStreamedMpEditor
  {
    public string localxml = string.Empty;
    string onup;
    string ondown;

    string bhSubMenuWriterV()
    {
      level1LateralBladeVisible = "Control.IsVisible(";
      level2LateralBladeVisible = "Control.IsVisible(";
      foreach (menuItem menItem in menuItems)
      {
        if (menItem.subMenuLevel1.Count > 0)
        {
          level1LateralBladeVisible += menItem.subMenuLevel1ID.ToString() + ")|Control.IsVisible(";

          if (menItem.subMenuLevel2.Count > 0)
            level1LateralBladeVisible += (menItem.subMenuLevel1ID + 100).ToString() + ")|Control.IsVisible(";

          writeSubMenuLevel1V(menItem);
        }
        if (menItem.subMenuLevel2.Count > 0)
        {
          level2LateralBladeVisible += (menItem.subMenuLevel1ID + 100).ToString() + ")|Control.IsVisible(";
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
                      "<animation effect=\"slide\" time=\"200\" start=\"-30,0\">visible</animation>" +
                      "<animation effect=\"slide\" time=\"0\" end=\"0,0\">hidden</animation>" +
                      "<animation effect=\"slide\" end=\"-800,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowClose</animation>" +
                      "<posX>" + int.Parse(txtMenuPos.Text) + "</posX>" +
                      "<posY>325</posY>" +
                      "<width>230</width>" +
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

        localxml += "<control>" +
                "<description>SUB ITEM " + j.ToString() + "</description>" +
                "<type>button</type>" +
                "<id>" + (parentMenu.subMenuLevel1ID + (j + 1)).ToString() + "</id>" +
                "<height>30</height>" +
                "<width>230</width>" +
                "<textXOff>5</textXOff>" +
                "<textYOff>4</textYOff>" +
                "<label>" + parentMenu.subMenuLevel1[j].displayName + "</label>";

       if (parentMenu.subMenuLevel1[j].hyperlink == "196299")
        localxml += "<action>99</action>";
       else if (parentMenu.subMenuLevel1[j].hyperlink == "196297")
        localxml += "<action>97</action>";
       else if (parentMenu.subMenuLevel1[j].hyperlink == "196250")
         localxml += "<action>196250</action>";
       else
        localxml += "<hyperlink>" + parentMenu.subMenuLevel1[j].hyperlink + "</hyperlink>";

       localxml += "<font>mediastream11tc</font>" +
                "<onleft>" + (parentMenu.id + 900).ToString() + "</onleft>" +
                "<onright>" + (parentMenu.subMenuLevel1ID + (j + 1 + isSecondLevel)).ToString() + "</onright>" +
                "<ondown>" + ondown + "</ondown>" +
                "<onup>" + onup + "</onup>" +
                "<textureFocus>listbg_fo.png</textureFocus>" +
                "<textureNoFocus>listbg_nf.png</textureNoFocus>" +
                "<visible allowhiddenfocus=\"true\">Control.IsVisible(" + parentMenu.subMenuLevel1ID.ToString() + ")</visible>" +
              "</control>";
      }
      localxml += "</control>";
      
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
                      "<animation effect=\"slide\" time=\"200\" start=\"-30,0\">visible</animation>" +
                      "<animation effect=\"slide\" time=\"0\" end=\"0,0\">hidden</animation>" +
                      "<animation effect=\"slide\" end=\"-800,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowClose</animation>" +
                      "<posX>" + (int.Parse(txtMenuPos.Text) + 230).ToString() + "</posX>" +
                      "<posY>325</posY>" +
                      "<width>230</width>" +
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

        localxml += "<control>" +
                "<description>SUB ITEM " + j.ToString() + "</description>" +
                "<type>button</type>" +
                "<id>" + (parentMenu.subMenuLevel1ID + (j + 101)).ToString() + "</id>" +
                "<height>30</height>" +
                "<width>230</width>" +
                "<textXOff>5</textXOff>" +
                "<textYOff>4</textYOff>" +
                "<label>" + parentMenu.subMenuLevel2[j].displayName + "</label>";

        if (parentMenu.subMenuLevel2[j].hyperlink == "196299")
          localxml += "<action>99</action>";
        else if (parentMenu.subMenuLevel2[j].hyperlink == "196297")
          localxml += "<action>97</action>";
        else if (parentMenu.subMenuLevel2[j].hyperlink == "196250")
          localxml += "<action>196250</action>";
        else
          localxml += "<hyperlink>" + parentMenu.subMenuLevel2[j].hyperlink + "</hyperlink>";


        localxml += "<font>mediastream11tc</font>" +
                 "<onleft>" + (parentMenu.subMenuLevel1ID + (j + 1)).ToString() + "</onleft>" +
                 "<onright>" + (parentMenu.subMenuLevel1ID + (j + 101)).ToString() + "</onright>" +
                 "<ondown>" + ondown + "</ondown>" +
                 "<onup>" + onup + "</onup>" +
                 "<textureFocus>listbg_fo.png</textureFocus>" +
                 "<textureNoFocus>listbg_nf.png</textureNoFocus>" +
                 "<visible allowhiddenfocus=\"true\">Control.IsVisible(" + (parentMenu.subMenuLevel1ID + 100).ToString() + ")</visible>" +
               "</control>";
      }
      localxml += "</control>";
      return localxml;
    }
  }
}
