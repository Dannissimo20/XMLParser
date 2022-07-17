using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

namespace Test;

public class ObjectRealty
{
    public int LayerID { get; set; }
    public int ObjectID { get; set; }
    public string CadastralNumber { get; set; }
    public string ObjType { get; set; }
    public string ObjTypeRus { get; set; }
    public string ObjectType { get; set; }
    public string CadastralBlock { get; set; }
    public string AssignationName { get; set; }
    public string Address { get; set; }
    public string? KeyType { get; set; }
    public string KeyValue { get; set; }
    public double CadastralCost { get; set; }
    public List<double> ListX { get; set; }
    public List<double> ListY { get; set; }
    public List<string> ListNumbers { get; set; }

    public ObjectRealty() { }
    public ObjectRealty(int layerID, int objectID, string cadastralNumber,string objType,string objTypeRus, string objectType, string cadastralBlock, string assignationName, string address, string? keyType, string keyValue, double cadastralCost, List<double> listX, List<double> listY, List<string> listNumbers)
    {
        LayerID = layerID;
        ObjectID = objectID;
        CadastralNumber = cadastralNumber;
        ObjType = objType;
        ObjTypeRus = objTypeRus;
        ObjectType = objectType;
        CadastralBlock = cadastralBlock;
        AssignationName = assignationName;
        Address = address;
        KeyType = keyType;
        KeyValue = keyValue;
        CadastralCost = cadastralCost;
        ListX = listX;
        ListY = listY;
        ListNumbers = listNumbers;
    }

    public static string ObjectGetAddress(XmlNode objectRealty)
    {
        XmlNode[] items = new XmlNode[1000];
        int i = 0;
        string resultAdr = "";
        foreach(XmlNode cn1 in objectRealty.ChildNodes)
        {
            if(cn1.Name == "Construction")
            {
                foreach (XmlNode cn2 in cn1.ChildNodes)
                {
                    if (cn2.Name == "Address")
                    {
                        ////i++;
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
    public static string ObjectGetCadastralBlock(XmlNode objectRealty)
    {
        XmlNode[] items = new XmlNode[1000];
        int i = 0;
        string result = "";
        foreach (XmlNode cn1 in objectRealty.ChildNodes)
        {
            if (cn1.Name == "CadastralBlock")
            {
                result = cn1.InnerText;
            }
        }
        return result;
    }
    public static double ObjectGetCadastralCost(XmlNode objectRealty)
    {
        XmlNode[] items = new XmlNode[1000];
        int i = 0;
        double result = 0.0;
        foreach(XmlNode cn1 in objectRealty.ChildNodes)
        {
            if(cn1.Name == "Construction")
            {
                foreach (XmlNode cn2 in cn1.ChildNodes)
                {
                    if (cn2.Name == "CadastralCost")
                    {
                        //i++;
                        items[i] = cn2;
                        foreach (XmlAttribute atr1 in items[i].Attributes)
                        {
                            if (atr1.Name == "Value")
                                result = double.Parse(atr1.InnerText.Replace(".", ","));
                        }
                    }
                }
            }
        }
        
        return result;
    }
    public static string ObjectGetCadastralNumber(XmlNode objectRealty)
    {
        string result = "";
        foreach(XmlNode cn1 in objectRealty.ChildNodes)
        {
            if(cn1.Name == "Construction")
            {
                foreach (XmlAttribute atr1 in cn1.Attributes)
                {
                    if (atr1.Name == "CadastralNumber")
                    {
                        result = atr1.InnerText;
                    }
                }
            }
        }
        
        return result;
    }
    public static string ObjectGetObjectType(XmlNode objectRealty)
    {
        string result = "";
        foreach (XmlNode cn1 in objectRealty.ChildNodes)
        {
            if (cn1.Name == "ObjectType")
            {
                result = DictionarysLib.ObjectTypeDictionary[cn1.InnerText];
            }
        }
        return result;
    }
    public static string ObjectGetAssignation(XmlNode objectRealty)
    {
        string result = "";
        foreach(XmlNode cn1 in objectRealty.ChildNodes)
        {
            if(cn1.Name == "Construction")
            {
                foreach (XmlNode cn2 in cn1.ChildNodes)
                {
                    if (cn2.Name == "AssignationName")
                    {
                        result = cn1.InnerText;
                    }
                }
            }
        }
        return result;
    }
    public static string ObjectGetKeyType(XmlNode objectRealty)
    {
        XmlNode[] items = new XmlNode[1000];
        int i = 0;
        string result = "";
        foreach (XmlNode cn1 in objectRealty.ChildNodes)
        {
            if (cn1.Name == "Construction")
            {
                foreach (XmlNode cn2 in cn1.ChildNodes)
                {
                    if (cn2.Name == "KeyParameters")
                    {
                        items[i] = cn2;
                        foreach (XmlNode cn3 in items[i].ChildNodes)
                        {
                            if (cn3.Name.Contains("KeyParameter"))
                            {
                                items[i] = cn3;
                                foreach (XmlAttribute cn4 in items[i].Attributes)
                                {
                                    if (cn4.Name == "Type")
                                        result = DictionarysLib.KeyTypeDictionary[cn4.InnerText];
                                }
                            }
                        }
                    }
                }
            }
        }
        return result;
    }
    public static string ObjectGetKeyValue(XmlNode objectRealty)
    {
        XmlNode[] items = new XmlNode[1000];
        int i = 0;
        string result = "";
        foreach (XmlNode cn1 in objectRealty.ChildNodes)
        {
            if (cn1.Name == "Construction")
            {
                foreach (XmlNode cn2 in cn1.ChildNodes)
                {
                    if (cn2.Name == "KeyParameters")
                    {
                        items[i] = cn2;
                        foreach (XmlNode cn3 in items[i].ChildNodes)
                        {
                            if (cn3.Name.Contains("KeyParameter"))
                            {
                                items[i] = cn3;
                                foreach (XmlAttribute cn4 in items[i].Attributes)
                                {
                                    if (cn4.Name == "Value")
                                        result = cn4.InnerText;
                                }
                            }
                        }
                    }
                }
            }
        }
        return result;
    }
    public static List<double> ObjectGetListX(XmlNode objectRealty)
    {
        XmlNode[] items = new XmlNode[10000000];
        int i = 0;
        List<double> result = new List<double>();
        foreach (XmlNode cn1 in objectRealty.ChildNodes)
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
        return result;
    }
    public static List<double> ObjectGetListY(XmlNode objectRealty)
    {
        XmlNode[] items = new XmlNode[10000000];
        int i = 0;
        List<double> result = new List<double>();
        foreach (XmlNode cn1 in objectRealty.ChildNodes)
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
        }
        return result;
    }
    public static List<string> ObjectGetListNumbers(XmlNode objectRealty)
    {
        XmlNode[] items = new XmlNode[10000000];
        int i = 0;
        List<string> result = new List<string>();
        foreach (XmlNode cn1 in objectRealty.ChildNodes)
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
        }
        return result;
    }
}
