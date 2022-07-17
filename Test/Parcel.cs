using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Test;
using System.Threading.Tasks;

namespace Test;

public class Parcel
{
    public int LayerID { get; set; }
    public int ObjectID { get; set; }
    public string CadastralNumber { get; set; }
    public double Area { get; set; }
    public string TypeObj { get; set; }
    public string TypeObjRus { get; set; }
    public string CadastralBlock { get; set; }
    public string Address { get; set; }
    public string Category { get; set; }
    public string? Utilization { get; set; }
    public string UtilizationBydoc { get; set; }
    public double CadastralCost { get; set; }
    public string State { get; set; }
    public string DateCreated { get; set; }
    public string SubFull { get; set; }
    public List<double>? ListX { get; set; }
    public List<double>? ListY { get; set; }
    public List<string>? ListNumbers { get; set; }
    public List<Parcel>? SubParcels { get; set; }
    public bool HasContours { get; set; }
    public int NumOfContours { get; set; }

    public Parcel() { }
    //SubParcel
    public Parcel(int layerID, int objectID, string cadastralNumber, double area, string typeObj, string typeObjRus, string cadastralBlock, string address, string category, string utilization, string utilizationBydoc, double cadastralCost, string state, string dateCreated, string subFull, List<double> listX, List<double> listY, List<string> listNumbers)
    {
        LayerID = layerID;
        ObjectID = objectID;
        CadastralNumber = cadastralNumber;
        Area = area;
        TypeObj = typeObj;
        TypeObjRus = typeObjRus;
        CadastralBlock = cadastralBlock;
        Address = address;
        Category = category;
        Utilization = utilization;
        UtilizationBydoc = utilizationBydoc;
        CadastralCost = cadastralCost;
        State = state;
        DateCreated = dateCreated;
        SubFull = subFull;
        ListX = listX;
        ListY = listY;
        ListNumbers = listNumbers;
    }
    //Parcel
    public Parcel(int layerID, int objectID, string cadastralNumber, double area, string typeObj, string typeObjRus, string cadastralBlock, string address, string category, string utilization, string utilizationBydoc, double cadastralCost, string state, string dateCreated, string subFull, List<double> listX, List<double> listY, List<string> listNumbers, List<Parcel> subParcels, bool hasContours, int numOfContours)
    {
        LayerID = layerID;
        ObjectID = objectID;
        CadastralNumber = cadastralNumber;
        Area = area;
        TypeObj = typeObj;
        TypeObjRus = typeObjRus;
        CadastralBlock = cadastralBlock;
        Address = address;
        Category = category;
        Utilization = utilization;
        UtilizationBydoc = utilizationBydoc;
        CadastralCost = cadastralCost;
        State = state;
        DateCreated = dateCreated;
        SubFull = subFull;
        ListX = listX;
        ListY = listY;
        ListNumbers = listNumbers;
        SubParcels = subParcels;
        HasContours = hasContours;
        NumOfContours = numOfContours;  
    }

    public static string ParcelGetAddress(XmlNode parcel)
    {
        XmlNode[] items = new XmlNode[1000];
        int i = 0;
        string resultAdr = "";
        foreach (XmlNode cn1 in parcel.ChildNodes)
        {
            if (cn1.Name == "Location")
            {
                items[i] = cn1;
                foreach (XmlNode cn2 in items[i].ChildNodes)
                {
                    if (cn2.Name == "Address")
                    {
                        items[i] = cn2;
                        foreach (XmlNode cn3 in items[i].ChildNodes)
                        {
                            if (cn3.Name.Contains("Region"))
                                resultAdr += cn3.InnerText + ", ";
                            if (cn3.Name.Contains("District"))
                                resultAdr += cn3.Attributes[1].InnerText + " " + cn3.Attributes[0].InnerText + ", ";
                            if (cn3.Name.Contains("City"))
                                resultAdr += cn3.Attributes[1].InnerText + " " + cn3.Attributes[0].InnerText + ", ";
                            if (cn3.Name.Contains("UrbanDistrict"))
                                resultAdr += cn3.Attributes[1].InnerText + " " + cn3.Attributes[0].InnerText + ", ";
                            if (cn3.Name.Contains("SovietVillage"))
                                resultAdr += cn3.Attributes[1].InnerText + " " + cn3.Attributes[0].InnerText + ", ";
                            if (cn3.Name.Contains("Locality"))
                                resultAdr += cn3.Attributes[1].InnerText + " " + cn3.Attributes[0].InnerText + ", ";
                            if (cn3.Name.Contains("Street"))
                                resultAdr += cn3.Attributes[1].InnerText + " " + cn3.Attributes[0].InnerText + ", ";
                            if (cn3.Name.Contains("Level1"))
                                resultAdr += cn3.Attributes[1].InnerText + " " + cn3.Attributes[0].InnerText + ", ";
                            if (cn3.Name.Contains("Level2"))
                                resultAdr += cn3.Attributes[1].InnerText + " " + cn3.Attributes[0].InnerText + ", ";
                            if (cn3.Name.Contains("Level3"))
                                resultAdr += cn3.Attributes[1].InnerText + " " + cn3.Attributes[0].InnerText + ", ";
                            if (cn3.Name.Contains("Apartment"))
                                resultAdr += cn3.Attributes[1].InnerText + " " + cn3.Attributes[0].InnerText + ", ";
                            if (cn3.Name.Contains("Other"))
                                resultAdr += cn3.InnerText + ", ";
                            if (cn3.Name.Contains("Note"))
                                resultAdr += cn3.InnerText;
                        }
                    }

                }
            }
        }
        return resultAdr;
    }
    public static List<XmlNode> ParcelGetSubParcels(XmlNode parcel)
    {
        XmlNode[] items = new XmlNode[1000];
        int i = 0;
        List<XmlNode> result = new List<XmlNode>();
        foreach (XmlNode cn1 in parcel.ChildNodes)
        {
            if (cn1.Name == "SubParcels")
            {
                items[i] = cn1;
                foreach (XmlNode cn2 in items[i].ChildNodes)
                {
                    result.Add(cn2);
                }
            }
        }
        return result;
    }
    public static string ParcelGetFull(XmlNode parcel)
    {
        XmlNode[] items = new XmlNode[1000];
        int i = 0;
        string result = "";
        foreach (XmlAttribute atr1 in parcel.Attributes)
        {
            if (atr1.Name == "Full")
            {
                result = atr1.InnerText;
            }
        }
        return result;
    }
    public static double ParcelGetArea(XmlNode parcel)
    {
        XmlNode[] items = new XmlNode[1000];
        int i = 0;
        double result = 0.0;
        foreach (XmlNode cn1 in parcel.ChildNodes)
        {
            if (cn1.Name == "Area")
            {
                //i++;
                items[i] = cn1;
                foreach (XmlNode cn2 in items[i].ChildNodes)
                {
                    if (cn2.Name == "Area")
                    {
                        result = double.Parse(cn2.InnerText.Replace(".", ","));
                    }
                }
            }
        }
        return result;
    }
    public static string ParcelGetCadastralBlock(XmlNode parcel)
    {
        XmlNode[] items = new XmlNode[1000];
        int i = 0;
        string result = "";
        foreach (XmlNode cn1 in parcel.ChildNodes)
        {
            if (cn1.Name == "CadastralBlock")
            {
                result = cn1.InnerText;
            }
        }
        return result;
    }
    public static string ParcelGetCategory(XmlNode parcel)
    {
        XmlNode[] items = new XmlNode[1000];
        int i = 0;
        string result = "";
        foreach (XmlNode cn1 in parcel.ChildNodes)
        {
            if (cn1.Name == "Category")
            {
                result = DictionarysLib.CategoryDictionary[cn1.InnerText];
            }
        }
        return result;
    }
    public static string ParcelGetUtilization(XmlNode parcel)
    {
        XmlNode[] items = new XmlNode[1000];
        int i = 0;
        string result = "";
        foreach (XmlNode cn1 in parcel.ChildNodes)
        {
            if (cn1.Name == "Utilization")
            {
                //i++;
                items[i] = cn1;
                foreach (XmlAttribute atr1 in cn1.Attributes)
                {
                    if (atr1.Name == "Utilization")
                        result = DictionarysLib.UtilizationDictionary[atr1.InnerText];
                }
            }
        }
        return result;
    }
    public static string ParcelGetUtilizationByDoc(XmlNode parcel)
    {
        XmlNode[] items = new XmlNode[1000];
        int i = 0;
        string result = "";
        foreach (XmlNode cn1 in parcel.ChildNodes)
        {
            if (cn1.Name == "Utilization")
            {
                //i++;
                items[i] = cn1;
                foreach (XmlAttribute atr1 in items[i].Attributes)
                {
                    if (atr1.Name == "ByDoc")
                        result = atr1.InnerText;
                }
            }
        }
        return result;
    }
    public static double ParcelGetCadastralCost(XmlNode parcel)
    {
        XmlNode[] items = new XmlNode[1000];
        int i = 0;
        double result = 0.0;
        foreach (XmlNode cn1 in parcel.ChildNodes)
        {
            if (cn1.Name == "CadastralCost")
            {
                //i++;
                items[i] = cn1;
                foreach (XmlAttribute atr1 in items[i].Attributes)
                {
                    if (atr1.Name == "Value")
                        result = double.Parse(atr1.InnerText.Replace(".", ","));
                }
            }
        }
        return result;
    }
    public static string ParcelGetCadastralNumber(XmlNode parcel)
    {
        XmlNode[] items = new XmlNode[1000];
        int i = 0;
        string result = "";
        foreach (XmlAttribute atr1 in parcel.Attributes)
        {
            if (atr1.Name == "CadastralNumber")
            {
                result = atr1.InnerText;
            }
        }
        return result;
    }
    public static string ParcelGetState(XmlNode parcel)
    {
        XmlNode[] items = new XmlNode[1000];
        int i = 0;
        string result = "";
        foreach (XmlAttribute atr1 in parcel.Attributes)
        {
            if (atr1.Name == "State")
            {
                result = DictionarysLib.StateDictionary[atr1.InnerText];
            }
        }
        if (result == "")
            result = "\"\"";
        return result;
    }
    public static string ParcelGetDateCreated(XmlNode parcel)
    {
        XmlNode[] items = new XmlNode[1000];
        int i = 0;
        string result = "";
        foreach (XmlAttribute atr1 in parcel.Attributes)
        {
            if (atr1.Name == "DateCreated")
            {
                result = atr1.InnerText;
            }
        }
        return result;
    }
    public static List<double> ParcelGetListX(XmlNode parcel)
    {
        XmlNode[] items = new XmlNode[10000000];
        int i = 0;
        List<double> result = new List<double>();
        foreach (XmlNode cn1 in parcel.ChildNodes)
        {
            if (cn1.Name == "EntitySpatial")
            {
                //i++;
                items[i] = cn1;
                foreach (XmlNode cn2 in items[i].ChildNodes)
                {
                    if (cn2.Name.Contains("SpatialElement"))
                    {
                        //i++;
                        items[i] = cn2;
                        foreach (XmlNode cn3 in items[i].ChildNodes)
                        {
                            if(cn3.Name.Contains("SpelementUnit"))
                            {
                                //i++;
                                items[i] = cn3;
                                foreach (XmlNode cn4 in items[i].ChildNodes)
                                {
                                    if (cn4.Name.Contains("Ordinate"))
                                    {
                                        //i++;
                                        items[i] = cn4;
                                        foreach(XmlAttribute atr1 in items[i].Attributes)
                                        {
                                            if (atr1.Name == "X")
                                            {
                                                result.Add(double.Parse(atr1.InnerText.Replace(".", ",")));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (cn1.Name == "Contours")
            {
                //i++;
                items[i] = cn1;
                foreach (XmlNode cn2 in items[i].ChildNodes)
                {
                    if (cn2.Name == "Contour")
                    {
                        //i++;
                        items[i] = cn2;
                        foreach (XmlNode cn3 in items[i].ChildNodes)
                        {
                            if (cn3.Name == "EntitySpatial")
                            {
                                //i++;
                                items[i] = cn3;
                                foreach (XmlNode cn4 in items[i].ChildNodes)
                                {
                                    if (cn4.Name.Contains("SpatialElement"))
                                    {
                                        //i++;
                                        items[i] = cn4;
                                        foreach (XmlNode cn5 in items[i].ChildNodes)
                                        {
                                            if (cn5.Name.Contains("SpelementUnit"))
                                            {
                                                //i++;
                                                items[i] = cn5;
                                                foreach (XmlNode cn6 in items[i].ChildNodes)
                                                {
                                                    if (cn6.Name.Contains("Ordinate"))
                                                    {
                                                        //i++;
                                                        items[i] = cn6;
                                                        foreach (XmlAttribute atr1 in items[i].Attributes)
                                                        {
                                                            if (atr1.Name == "X")
                                                            {
                                                                result.Add(double.Parse(atr1.InnerText.Replace(".", ",")));
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return result;
    }
    public static List<double> ParcelGetListY(XmlNode parcel)
    {
        XmlNode[] items = new XmlNode[10000000];
        int i = 0;
        List<double> result = new List<double>();
        foreach (XmlNode cn1 in parcel.ChildNodes)
        {
            if (cn1.Name == "EntitySpatial")
            {
                //i++;
                items[i] = cn1;
                foreach (XmlNode cn2 in items[i].ChildNodes)
                {
                    if (cn2.Name.Contains("SpatialElement"))
                    {
                        //i++;
                        items[i] = cn2;
                        foreach (XmlNode cn3 in items[i].ChildNodes)
                        {
                            if (cn3.Name.Contains("SpelementUnit"))
                            {
                                //i++;
                                items[i] = cn3;
                                foreach (XmlNode cn4 in items[i].ChildNodes)
                                {
                                    if (cn4.Name.Contains("Ordinate"))
                                    {
                                        //i++;
                                        items[i] = cn4;
                                        foreach (XmlAttribute atr1 in items[i].Attributes)
                                        {
                                            if (atr1.Name == "Y")
                                            {
                                                result.Add(double.Parse(atr1.InnerText.Replace(".", ",")));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (cn1.Name == "Contours")
            {
                //i++;
                items[i] = cn1;
                foreach (XmlNode cn2 in items[i].ChildNodes)
                {
                    if (cn2.Name == "Contour")
                    {
                        //i++;
                        items[i] = cn2;
                        foreach (XmlNode cn3 in items[i].ChildNodes)
                        {
                            if (cn3.Name == "EntitySpatial")
                            {

                                //i++;
                                items[i] = cn3;
                                foreach (XmlNode cn4 in items[i].ChildNodes)
                                {
                                    if (cn4.Name.Contains("SpatialElement"))
                                    {
                                        //i++;
                                        items[i] = cn4;
                                        foreach (XmlNode cn5 in items[i].ChildNodes)
                                        {
                                            if (cn5.Name.Contains("SpelementUnit"))
                                            {
                                                //i++;
                                                items[i] = cn5;
                                                foreach (XmlNode cn6 in items[i].ChildNodes)
                                                {
                                                    if (cn6.Name.Contains("Ordinate"))
                                                    {
                                                        //i++;
                                                        items[i] = cn6;
                                                        foreach (XmlAttribute atr1 in items[i].Attributes)
                                                        {
                                                            if (atr1.Name == "Y")
                                                            {
                                                                result.Add(double.Parse(atr1.InnerText.Replace(".", ",")));
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return result;
    }
    public static List<string> ParcelGetListNumbers(XmlNode parcel)
    {
        XmlNode[] items = new XmlNode[10000000];
        int i = 0;
        List<string> result = new List<string>();
        foreach (XmlNode cn1 in parcel.ChildNodes)
        {
            if (cn1.Name == "EntitySpatial")
            {
                //i++;
                items[i] = cn1;
                foreach (XmlNode cn2 in items[i].ChildNodes)
                {
                    if (cn2.Name.Contains("SpatialElement"))
                    {
                        //i++;
                        items[i] = cn2;
                        foreach (XmlNode cn3 in items[i].ChildNodes)
                        {
                            if (cn3.Name.Contains("SpelementUnit"))
                            {
                                //i++;
                                items[i] = cn3;
                                foreach (XmlAttribute atr1 in items[i].Attributes)
                                {
                                    if (atr1.Name == "SuNmb")
                                        result.Add(atr1.InnerText);
                                }
                            }
                        }
                    }
                }
            }
            if (cn1.Name == "Contours")
            {
                //i++;
                items[i] = cn1;
                foreach (XmlNode cn2 in items[i].ChildNodes)
                {
                    if (cn2.Name == "Contour")
                    {
                        //i++;
                        items[i] = cn2;
                        foreach (XmlNode cn3 in items[i].ChildNodes)
                        {
                            if (cn3.Name == "EntitySpatial")
                            {
                                //i++;
                                items[i] = cn3;
                                foreach (XmlNode cn4 in items[i].ChildNodes)
                                {
                                    if (cn4.Name.Contains("SpatialElement"))
                                    {
                                        //i++;
                                        items[i] = cn4;
                                        foreach (XmlNode cn5 in items[i].ChildNodes)
                                        {
                                            if (cn5.Name.Contains("SpelementUnit"))
                                            {
                                                //i++;
                                                items[i] = cn5;
                                                foreach (XmlAttribute atr1 in items[i].Attributes)
                                                {
                                                    if (atr1.Name == "SuNmb")
                                                        result.Add(atr1.InnerText);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
        }
        return result;
    }
    public static bool IsParcelGetContours(XmlNode parcel)
    {
        bool i = false;
        foreach (XmlNode cn1 in parcel.ChildNodes)
        {
            if (cn1.Name == "Contours")
                i=true;
        }
        return i;
    }
    public static int ParcelNumOfContours(XmlNode parcel)
    {
        int i = 0;
        foreach (XmlNode cn1 in parcel.ChildNodes)
        {
            if (cn1.Name == "Contours")
                i = cn1.ChildNodes.Count;
        }
        return i;
    }
}
