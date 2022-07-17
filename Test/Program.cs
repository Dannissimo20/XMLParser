using System.Xml;
using System.Text;
using Test;

public class Program
{
    public static void Main()
    {
        Parse();
    }

    public static void Parse()
    {
        double maxX;
        double maxY;
        double minX;
        double minY;
        int ID = 1;

        //Список кварталов
        List<Parcel> parcels = new List<Parcel>();
        List<ObjectRealty> objects = new List<ObjectRealty>();
        var XMLsDir = Directory.GetFiles("../../../XMLs");
        for (int FilesCount = 0; FilesCount < XMLsDir.Length; FilesCount++)
        {
            if (XMLsDir[FilesCount].Substring(XMLsDir[FilesCount].Length - 4) == ".xml")
            {
                //Сам .xml документ
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load($"{XMLsDir[FilesCount]}");

                //Получаем корень .xml файла (<KVZU>)
                XmlElement? xRoot = xDoc.DocumentElement;

                //Лист элементов Parcel
                var xmlParcels = xRoot.GetElementsByTagName("Parcel");
                var xmlObjects = xRoot.GetElementsByTagName("ObjectRealty");

                #region Заполнение листа с Parcel
                if (xmlParcels != null)
                {
                    for (int i = 0; i < xmlParcels.Count; i++)
                    {
                        #region Формирование списка SubParcel
                        List<Parcel>? subParcels = new List<Parcel>();
                        List<XmlNode> xmlSubParcels = Parcel.ParcelGetSubParcels(xmlParcels[i]);
                        if (xmlSubParcels != null)
                        {
                            for (int j = 0; j < xmlSubParcels.Count; j++)
                            {
                                #region Создание объекта SubParcel
                                //Кратенькая проверочка на CadastralBlock, где то он в атрибуте, где то текстом внутри тега
                                var scb = Parcel.ParcelGetCadastralBlock(xmlParcels[i]);
                                if (scb == "")
                                    scb = xRoot.GetElementsByTagName("CadastralBlock")[0].Attributes[0].InnerText;

                                Parcel subParcel = new Parcel(1,
                                    ID,
                                    Parcel.ParcelGetCadastralNumber(xmlParcels[i]) + $"/{j + 1}",
                                    Parcel.ParcelGetArea(xmlSubParcels[j]),
                                    "s",
                                    DictionarysLib.ObjectDictionary["s"],
                                    scb,
                                    Parcel.ParcelGetAddress(xmlParcels[i]),
                                    Parcel.ParcelGetCategory(xmlParcels[i]),
                                    Parcel.ParcelGetUtilization(xmlParcels[i]),
                                    Parcel.ParcelGetUtilizationByDoc(xmlParcels[i]),
                                    Parcel.ParcelGetCadastralCost(xmlParcels[i]),
                                    Parcel.ParcelGetState(xmlSubParcels[j]),
                                    DateTime.Parse(Parcel.ParcelGetDateCreated(xmlParcels[i])).ToString("d"),
                                    Parcel.ParcelGetFull(xmlSubParcels[j]),
                                    Parcel.ParcelGetListX(xmlSubParcels[j]),
                                    Parcel.ParcelGetListY(xmlSubParcels[j]),
                                    Parcel.ParcelGetListNumbers(xmlSubParcels[j]));
                                #endregion

                                subParcels.Add(subParcel);
                                ID++;
                            }
                        }
                        #endregion

                        //Кратенькая проверочка на CadastralBlock, где то он в атрибуте, где то текстом внутри тега
                        Parcel parcel;
                        var cb = Parcel.ParcelGetCadastralBlock(xmlParcels[i]);
                        if (cb == "")
                            cb = xRoot.GetElementsByTagName("CadastralBlock")[0].Attributes[0].InnerText;
                        var hasContoures = false;
                        int numOfContoures = 0;
                        if (Parcel.IsParcelGetContours(xmlParcels[i]))
                        {
                            hasContoures = true;
                            numOfContoures = Parcel.ParcelNumOfContours(xmlParcels[i]);
                            for (int k = 0; k < numOfContoures; k++)
                            {
                                parcel = new Parcel(1,
                                ID,
                                Parcel.ParcelGetCadastralNumber(xmlParcels[i]) + $"({k + 1})",
                                0,
                                "c",
                                DictionarysLib.ObjectDictionary["c"],
                                cb,
                                Parcel.ParcelGetAddress(xmlParcels[i]),
                                Parcel.ParcelGetCategory(xmlParcels[i]),
                                Parcel.ParcelGetUtilization(xmlParcels[i]),
                                Parcel.ParcelGetUtilizationByDoc(xmlParcels[i]),
                                Parcel.ParcelGetCadastralCost(xmlParcels[i]),
                                Parcel.ParcelGetState(xmlParcels[i]),
                                DateTime.Parse(Parcel.ParcelGetDateCreated(xmlParcels[i])).ToString("d"),
                                "",
                                Parcel.ParcelGetListX(xmlParcels[i]),
                                Parcel.ParcelGetListY(xmlParcels[i]),
                                Parcel.ParcelGetListNumbers(xmlParcels[i]),
                                subParcels,
                                hasContoures,
                                numOfContoures);
                                parcels.Add(parcel);
                                ID++;
                            }
                        }
                        #region Создание объекта Parcel
                        parcel = new Parcel(1,
                            ID,
                            Parcel.ParcelGetCadastralNumber(xmlParcels[i]),
                            Parcel.ParcelGetArea(xmlParcels[i]),
                            "p",
                            DictionarysLib.ObjectDictionary["p"],
                            cb,
                            Parcel.ParcelGetAddress(xmlParcels[i]),
                            Parcel.ParcelGetCategory(xmlParcels[i]),
                            Parcel.ParcelGetUtilization(xmlParcels[i]),
                            Parcel.ParcelGetUtilizationByDoc(xmlParcels[i]),
                            Parcel.ParcelGetCadastralCost(xmlParcels[i]),
                            Parcel.ParcelGetState(xmlParcels[i]),
                            DateTime.Parse(Parcel.ParcelGetDateCreated(xmlParcels[i])).ToString("d"),
                            "",
                            Parcel.ParcelGetListX(xmlParcels[i]),
                            Parcel.ParcelGetListY(xmlParcels[i]),
                            Parcel.ParcelGetListNumbers(xmlParcels[i]),
                            subParcels,
                            hasContoures,
                            numOfContoures);
                        #endregion

                        if (parcel.ListNumbers.Count > 0)
                        {
                            parcels.Add(parcel);
                            ID++;
                        }
                    }
                }
                #endregion

                #region Формирование списка сооружений
                if (xmlObjects != null)
                {
                    for (int i = 0; i < xmlObjects.Count; i++)
                    {
                        ObjectRealty obj;
                        var cb = Parcel.ParcelGetCadastralBlock(xmlParcels[i]);
                        if (cb == "")
                            cb = xRoot.GetElementsByTagName("CadastralBlock")[0].Attributes[0].InnerText;
                        obj = new ObjectRealty(1,
                            ID,
                            ObjectRealty.ObjectGetCadastralNumber(xmlObjects[i]),
                            "t",
                            DictionarysLib.ObjectDictionary["t"],
                            ObjectRealty.ObjectGetObjectType(xmlObjects[i]),
                            cb,
                            ObjectRealty.ObjectGetAssignation(xmlObjects[i]),
                            ObjectRealty.ObjectGetAddress(xmlObjects[i]),
                            ObjectRealty.ObjectGetKeyType(xmlObjects[i]),
                            ObjectRealty.ObjectGetKeyValue(xmlObjects[i]),
                            ObjectRealty.ObjectGetCadastralCost(xmlObjects[i]),
                            ObjectRealty.ObjectGetListX(xmlObjects[i]),
                            ObjectRealty.ObjectGetListY(xmlObjects[i]),
                            ObjectRealty.ObjectGetListNumbers(xmlObjects[i]));
                        objects.Add(obj);
                        ID++;
                    }
                }
                #endregion

                #region Поиск наибольшей и наименьшей пары координат
                List<double> allX = new List<double>();
                List<double> allY = new List<double>();
                for (int i = 0; i < parcels.Count; i++)
                {
                    for (int j = 0; j < parcels[i].SubParcels.Count; j++)
                        for (int k = 0; k < parcels[i].SubParcels[j].ListNumbers.Count; k++)
                        {
                            allX.Add(parcels[i].SubParcels[j].ListX[k]);
                            allY.Add(parcels[i].SubParcels[j].ListY[k]);
                        }
                    for (int h = 0; h < parcels[i].ListNumbers.Count; h++)
                    {
                        allX.Add(parcels[i].ListX[h]);
                        allY.Add(parcels[i].ListY[h]);
                    }
                }
                maxX = allX.Max();
                maxY = allY.Max();
                minX = allX.Min();
                minY = allY.Min();
                #endregion

                #region Формирование ".mif" файла
                string mifText = "";
                //Заполнение данных для .mid (столбцы)
                mifText = "Version 300\n" +
                                "Charset \"WindowsCyrillic\"\n" +
                                "Delimiter \",\"\n" +
                               $"CoordSys NonEarth Units \"m\" Bounds ({minX}, {minY}) ({maxX}, {maxY})\n" +
                                "Columns 15\n" +
                                "  LayerID Integer\n" +
                                "  ObjectID Integer\n" +
                                "  CadastralNumber Char(40)\n" +
                                "  Area Decimal(20, 2)\n" +
                                "  TypeObj Char(1)\n" +
                                "  TypeObjRus Char(50)\n" +
                                "  CadastralBlock Char(40)\n" +
                                "  Address Char(254)\n" +
                                "  Category Char(254)\n" +
                                "  Utilization Char(254)\n" +
                                "  UtilizationBydoc Char(254)\n" +
                                "  CadastralCost Decimal(20, 2)\n" +
                                "  State Char(20)\n" +
                                "  DateCreated Date\n" +
                                "  SubFull Char(1)\n" +
                                "Data\n" +
                                "\n";

                //Заполнение регионов
                for (int i = 0; i < parcels.Count; i++)
                {
                    for (int j = 0; j < parcels[i].SubParcels.Count; j++)
                    {
                        mifText += "Region 1\n" +
                                    $"{parcels[i].SubParcels[j].ListNumbers.Count + 1}\n";
                        for (int k = 0; k < parcels[i].SubParcels[j].ListNumbers.Count; k++)
                            mifText += $"{parcels[i].SubParcels[j].ListY[k].ToString().Replace(",", ".")} {parcels[i].SubParcels[j].ListX[k].ToString().Replace(",", ".")}\n";
                        mifText += $"{parcels[i].SubParcels[j].ListY[0].ToString().Replace(",", ".")} {parcels[i].SubParcels[j].ListX[0].ToString().Replace(",", ".")}\n";
                        mifText += "PEN (1, 4, 16766720)\n" +
                                     "BRUSH (5, 16766720)\n";
                    }
                    mifText += "Region 1\n" +
                                $"{parcels[i].ListNumbers.Count + 1}\n";
                    for (int h = 0; h < parcels[i].ListNumbers.Count; h++)
                        mifText += $"{parcels[i].ListY[h].ToString().Replace(",", ".")} {parcels[i].ListX[h].ToString().Replace(",", ".")}\n";
                    if (!parcels[i].HasContours)
                        mifText += $"{parcels[i].ListY[0].ToString().Replace(",", ".")} {parcels[i].ListX[0].ToString().Replace(",", ".")}\n";
                    mifText += "PEN (2, 2, 32768)\n" +
                                 "BRUSH (5, 32768)\n";
                }

                for (int i = 0; i < objects.Count; i++)
                {
                    mifText += $"PLINE {objects[i].ListNumbers.Count + 1}\n";
                    for (int h = 0; h < objects[i].ListNumbers.Count; h++)
                        mifText += $"{objects[i].ListY[h].ToString().Replace(",", ".")} {objects[i].ListX[h].ToString().Replace(",", ".")}\n";
                    mifText += "PEN (PEN (2, 2, 16711680))\n";
                }



                for (int i = 0; i < parcels.Count; i++)
                {
                    for (int j = 0; j < parcels[i].SubParcels.Count; j++)
                        for (int k = 0; k < parcels[i].SubParcels[j].ListNumbers.Count; k++)
                            mifText += $"Point {parcels[i].SubParcels[j].ListY[k].ToString().Replace(",", ".")} {parcels[i].SubParcels[j].ListX[k].ToString().Replace(",", ".")}\n";
                    for (int h = 0; h < parcels[i].ListNumbers.Count; h++)
                        mifText += $"Point {parcels[i].ListY[h].ToString().Replace(",", ".")} {parcels[i].ListX[h].ToString().Replace(",", ".")}\n";
                }
                for (int i = 0; i < objects.Count; i++)
                {
                    for (int h = 0; h < objects[i].ListNumbers.Count; h++)
                        mifText += $"Point {objects[i].ListY[h].ToString().Replace(",", ".")} {objects[i].ListX[h].ToString().Replace(",", ".")}\n";
                }
                #endregion

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                File.WriteAllText(@$"../../../Results/{XMLsDir[FilesCount].Remove(0, 14)}.mif", mifText, Encoding.GetEncoding(1251));

                #region Формирование ".mid" файла
                string midText = "";
                for (int i = 0; i < parcels.Count; i++)
                {
                    for (int j = 0; j < parcels[i].SubParcels.Count; j++)
                    {
                        midText += $"{parcels[i].SubParcels[j].LayerID}," +
                                     $"{parcels[i].SubParcels[j].ObjectID}," +
                                     $"\"{parcels[i].SubParcels[j].CadastralNumber}\"," +
                                     $"{parcels[i].SubParcels[j].Area}," +
                                     $"\"{parcels[i].SubParcels[j].TypeObj}\"," +
                                     $"\"{parcels[i].SubParcels[j].TypeObjRus}\"," +
                                     $"\"{parcels[i].SubParcels[j].CadastralBlock}\"," +
                                     $"\"{parcels[i].SubParcels[j].Address}\"," +
                                     $"\"{parcels[i].SubParcels[j].Category}\"," +
                                     $"\"{parcels[i].SubParcels[j].Utilization}\"," +
                                     $"\"{parcels[i].SubParcels[j].UtilizationBydoc}\"," +
                                     $"{parcels[i].SubParcels[j].CadastralCost.ToString().Replace(",", ".")}," +
                                     $"\"{parcels[i].SubParcels[j].State}\"," +
                                     $"\"{parcels[i].SubParcels[j].DateCreated}\"," +
                                     $"\"{parcels[i].SubParcels[j].SubFull}\"\n";
                    }
                    if (parcels[i].HasContours)
                    {
                        midText += $"{parcels[i].LayerID}," +
                                     $"{parcels[i].ObjectID}," +
                                     $"\"{parcels[i].CadastralNumber}\"," +
                                     $"{parcels[i].Area}," +
                                     $"\"{parcels[i].TypeObj}\"," +
                                     $"\"{parcels[i].TypeObjRus}\"," +
                                     $"\"{parcels[i].CadastralBlock}\"," +
                                     $"\"{parcels[i].Address}\"," +
                                     $"\"{parcels[i].Category}\"," +
                                     $"\"{parcels[i].Utilization}\"," +
                                     $"\"{parcels[i].UtilizationBydoc}\"," +
                                     $"{parcels[i].CadastralCost.ToString().Replace(",", ".")}," +
                                     $"\"{parcels[i].State}\"," +
                                     $"\"{parcels[i].DateCreated}\"," +
                                     $"\"{parcels[i].SubFull}\"\n";
                    }
                    else
                    {
                        midText += $"{parcels[i].LayerID}," +
                                     $"{parcels[i].ObjectID}," +
                                     $"\"{parcels[i].CadastralNumber}\"," +
                                     $"{parcels[i].Area}," +
                                     $"\"{parcels[i].TypeObj}\"," +
                                     $"\"{parcels[i].TypeObjRus}\"," +
                                     $"\"{parcels[i].CadastralBlock}\"," +
                                     $"\"{parcels[i].Address}\"," +
                                     $"\"{parcels[i].Category}\"," +
                                     $"\"{parcels[i].Utilization}\"," +
                                     $"\"{parcels[i].UtilizationBydoc}\"," +
                                     $"{parcels[i].CadastralCost.ToString().Replace(",", ".")}," +
                                     $"\"{parcels[i].State}\"," +
                                     $"\"{parcels[i].DateCreated}\"," +
                                     $"\"{parcels[i].SubFull}\"\n";
                    }
                }
                for (int i = 0; i < objects.Count; i++)
                {
                    midText += $"{objects[i].LayerID}," +
                                 $"{objects[i].ObjectID}," +
                                 $"\"{objects[i].CadastralNumber}\"," +
                                 $"0," +
                                 $"\"{objects[i].ObjType}\"," +
                                 $"\"{objects[i].ObjTypeRus}\"," +
                                 $"\"{objects[i].CadastralBlock}\"," +
                                 $"\"{objects[i].Address}\"," +
                                 $"{objects[i].CadastralCost}," +
                                 $"\"{objects[i].AssignationName}\"," +
                                 $"\"{objects[i].KeyType}\"," +
                                 $"{objects[i].KeyValue}\n";
                }

                for (int i = 0; i < parcels.Count; i++)
                {
                    for (int j = 0; j < parcels[i].SubParcels.Count; j++)
                        for (int k = 0; k < parcels[i].SubParcels[j].ListNumbers.Count - 1; k++)
                            midText += $"2," +
                                         $"{parcels[i].SubParcels[j].ObjectID}," +
                                         $"\"{parcels[i].SubParcels[j].ListNumbers[k]}\"," +
                                         $"0," +
                                         $"\"{parcels[i].SubParcels[j].TypeObj}\"," +
                                         $"\"{parcels[i].SubParcels[j].TypeObjRus}\"\n";
                    if (parcels[i].HasContours)
                    {
                        for (int j = 0; j < parcels[i].NumOfContours; j++)
                        {
                            midText += $"2," +
                                     $"{parcels[i].ObjectID}," +
                                     $"\"{parcels[i].ListNumbers[j]}\"," +
                                     $"0," +
                                     $"\"{parcels[i].TypeObj}\"," +
                                     $"\"{parcels[i].TypeObjRus}\"\n";
                        }
                    }

                    for (int h = 0; h < parcels[i].ListNumbers.Count - 1; h++)
                        midText += $"2," +
                                     $"{parcels[i].ObjectID}," +
                                     $"\"{parcels[i].ListNumbers[h]}\"," +
                                     $"0," +
                                     $"\"{parcels[i].TypeObj}\"," +
                                     $"\"{parcels[i].TypeObjRus}\"\n";
                }
                for (int i = 0; i < objects.Count; i++)
                {
                    midText += $"2," +
                                 $"{objects[i].ObjectID}," +
                                 $"\"{objects[i].ListNumbers}\"," +
                                 $"0" +
                                 $"\"{objects[i].ObjType}\"," +
                                 $"\"{objects[i].ObjTypeRus}\"\n";
                }
                #endregion

                File.WriteAllText(@$"../../../Results/{XMLsDir[FilesCount].Remove(0, 14)}.mid", midText, Encoding.GetEncoding(1251));
                mifText = "";
                midText = "";
                parcels.Clear();
                ID = 1;
                maxX = 0;
                maxY = 0;
                minX = 0;
                minY = 0;
                allX.Clear();
                allY.Clear();
            }
            XMLsDir[FilesCount] = "";
        }
    }
}