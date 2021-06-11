using System.IO;

namespace FLTD_lib
{
    public partial class FLTD
    {
        public bool DumpData(string filePath)
        {
            using (var fp = new StreamWriter(new FileStream(filePath, FileMode.Create, FileAccess.Write)))
            {
                fp.WriteLine("NIFL\n");
                fp.WriteLine($"real address         = [0]\n");
                fp.WriteLine($"tag                  = [{mStNIFL.mNIFL.tag:x8}]");
                fp.WriteLine($"reserve0             = [{mStNIFL.mNIFL.reserve0:x8}]");
                fp.WriteLine($"reserve1             = [{mStNIFL.mNIFL.reserve1:x8}]");
                fp.WriteLine($"rel0_offset          = [{mStNIFL.mNIFL.rel0_offset:x8}]");
                fp.WriteLine($"rel0_size            = [{mStNIFL.mNIFL.rel0_size:x8}]");
                fp.WriteLine($"nof0_offset          = [{mStNIFL.mNIFL.nof0_offset:x8}]");
                fp.WriteLine($"nof0_size            = [{mStNIFL.mNIFL.nof0_size:x8}]");
                fp.WriteLine($"reserve0             = [{mStNIFL.mNIFL.reserve2:x8}]");
                fp.WriteLine("\n");

                fp.WriteLine("REL0\n");
                fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset:x8}]\n");
                fp.WriteLine($"tag                  = [{mStNIFL.mREL0.tag:x8}]");
                fp.WriteLine($"size                 = [{mStNIFL.mREL0.size:x8}]");
                fp.WriteLine($"entry                = [{mStNIFL.mREL0.entry:x8}]");
                fp.WriteLine($"reserve              = [{mStNIFL.mREL0.reserve:x8}]");
                fp.WriteLine("\n");

                fp.WriteLine("NOF0\n");
                fp.WriteLine($"real address         = [{mStNIFL.mNIFL.nof0_offset:x8}]\n");
                fp.WriteLine($"tag                  = [{mStNIFL.mNOF0.tag:x8}]");
                fp.WriteLine($"size                 = [{mStNIFL.mNOF0.size:x8}]");
                fp.WriteLine($"count                = [{mStNIFL.mNOF0.count:x8}]");
                fp.WriteLine("\nNOF0 Address List");
                uint[] addresses = mStNIFL.mNOF0.GetOriginalAddresses(mStNIFL.mNIFL.rel0_offset);
                for (int i = 0; i < addresses.Length; i++)
                {
                    fp.WriteLine($"{i:x4}  [{mStNIFL.mNOF0.addresses[i]:x8}] => [{addresses[i]:x8}]");
                }
                fp.WriteLine("\n");

                fp.WriteLine("FLTD\n");
                fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset + mStNIFL.mREL0.entry:x8}]\n");
                fp.WriteLine($"format               = [{data_array.format:x8}]");
                fp.WriteLine($"data0 count          = [{data_array.count_addr0:x8}]");
                fp.WriteLine($"data1 count          = [{data_array.count_addr1:x8}]");
                fp.WriteLine($"idk                  = [{data_array.idk3:x8}]");
                fp.WriteLine($"data0 offset         = [{data_array.fltd_data0_addr:x8}]");
                fp.WriteLine($"data1 offset         = [{data_array.fltd_data1_addr:x8}]");
                fp.WriteLine($"reserve              = [{data_array.reserve:x8}]");
                fp.WriteLine($"data2 offset         = [{data_array.fltd_data2_addr:x8}]");
                fp.WriteLine("\n");

                if (data_array.IsNGS() == true)
                {
                    NGS.fltd_data0[] data0 = (NGS.fltd_data0[])data_array.data0;
                    NGS.fltd_data1[] data1 = (NGS.fltd_data1[])data_array.data1;
                    NGS.fltd_data2 data2 = (NGS.fltd_data2)data_array.data2;

                    fp.WriteLine("data0\n");
                    for (int i = 0; i < data0.Length; i++)
                    {
                        fp.WriteLine($"***** {i:x4}({i}) *****");
                        fp.WriteLine("data0\n");
                        fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset + data_array.fltd_data0_addr + i * NGS.fltd_data0.GetMyDataSize():x8}]\n");
                        fp.WriteLine($"index                = [{data0[i].index:x8}]");
                        fp.WriteLine($"name address count   = [{data0[i].count_addr0:x8}]");
                        fp.WriteLine($"data3 count          = [{data0[i].count_addr1:x8}]");
                        fp.WriteLine($"reserve              = [{data0[i].reserve:x8}]");
                        fp.WriteLine($"name address offset  = [{data0[i].name_address_address:x8}]");
                        fp.WriteLine($"data3 offset         = [{data0[i].fltd_data3_addr:x8}]");
                        fp.WriteLine($"value0               = [{data0[i].value0:x8}]");
                        fp.WriteLine($"value1               = [{data0[i].value1:x8}]");
                        fp.WriteLine("\naddress and name");
                        for (int j = 0; j < data0[i].count_addr0; j++)
                        {
                            fp.WriteLine($"[{data0[i].nameAddressList[j]:x8}]{data0[i].nameList[j]}");
                        }
                        fp.WriteLine("");
                    }
                    fp.WriteLine("\n");

                    fp.WriteLine("data1\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        fp.WriteLine($"***** {i:x4}({i}) *****");
                        fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset + data_array.fltd_data1_addr + i * NGS.fltd_data1.GetMyDataSize():x8}]\n");
                        fp.WriteLine($"idk0                 = [{data1[i].idk0:x8}]");
                        fp.WriteLine($"data13 count         = [{data1[i].count_fltd_data13:x8}]");
                        fp.WriteLine($"idk2                 = [{data1[i].count_addr1:x8}]");
                        fp.WriteLine($"idk3                 = [{data1[i].idk3:x8}]");
                        fp.WriteLine($"data4 count          = [{data1[i].count_addr0:x8}]");
                        fp.WriteLine($"data5 count          = [{data1[i].count_addr1:x8}]");
                        fp.WriteLine($"idk6                 = [{data1[i].idk6:x8}]");
                        fp.WriteLine($"idk7                 = [{data1[i].idk7:x8}]");
                        fp.WriteLine($"data4 offset         = [{data1[i].fltd_data4_addr:x8}]");
                        fp.WriteLine($"data5 offset         = [{data1[i].fltd_data5_addr:x8}]");
                        fp.WriteLine($"data6 offset         = [{data1[i].fltd_data6_addr:x8}]");
                        fp.WriteLine($"value0               = [{data1[i].value0:x8}]");
                        fp.WriteLine($"value1               = [{data1[i].value1:x8}]");
                        fp.WriteLine("");
                    }
                    fp.WriteLine("\n");

                    fp.WriteLine("data2\n");
                    fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset + data_array.fltd_data2_addr:x8}]\n");
                    fp.WriteLine($"value0               = [{data2.value0:x8}]");
                    fp.WriteLine($"fin address?         = [{data2.address0:x8}]");
                    fp.WriteLine($"value1               = [{data2.value1:x8}]");
                    fp.WriteLine($"value2               = [{data2.value2:x8}]");

                    fp.WriteLine("\n");

                    fp.WriteLine("data3\n");
                    for (int h = 0; h < data0.Length; h++)
                    {
                        for (int i = 0; i < data0[h].data3.Length; i++)
                        {
                            fp.WriteLine($"***** {i:x4}({i}) *****");
                            fp.WriteLine($"real address       = [{mStNIFL.mNIFL.rel0_offset + data0[h].fltd_data3_addr + i * NGS.fltd_data3.GetMyDataSize():x8}]\n");
                            fp.WriteLine($"unk0               = [{data0[h].data3[i].idk0:x8}]");
                            fp.WriteLine($"unk1               = [{data0[h].data3[i].idk1:x8}]");
                            fp.WriteLine($"unk2               = [{data0[h].data3[i].idk2:x8}]");
                            fp.WriteLine($"unk3               = [{data0[h].data3[i].idk3:x8}]");
                            fp.WriteLine($"value0             =");
                            for (int k = 0; k < 0xe; k++)
                            {
                                fp.WriteLine($"{data0[h].data3[i].value0[k]}");
                            }
                            fp.WriteLine("");
                            fp.WriteLine($"reserve0         = [data0[h].data3[i].reserve0:x8]");
                            fp.WriteLine($"reserve1         = [data0[h].data3[i].reserve1:x8]");

                            fp.WriteLine($"name offset      = [{data0[h].data3[i].name_address0:x8}]({data0[h].data3[i].name})");
                            fp.WriteLine($"address1         = [{data0[h].data3[i].address1:x8}]");
                            fp.WriteLine($"address2         = [{data0[h].data3[i].address2:x8}]");
                            fp.WriteLine($"address3         = [{data0[h].data3[i].address3:x8}]");

                            fp.WriteLine($"value5           = [{data0[h].data3[i].value5:x8}]");
                            fp.WriteLine($"value6           = [{data0[h].data3[i].value6:x8}]");
                            fp.WriteLine("");
                        }
                    }

                    fp.WriteLine("\n");
                    fp.WriteLine("data4\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data4.Length; j++)
                        {
                            fp.WriteLine($"***** {i:x4}({i}) *****");
                            fp.WriteLine($"real address       = [{mStNIFL.mNIFL.rel0_offset + data1[i].fltd_data4_addr + j * NGS.fltd_data4.GetMyDataSize():x8}]\n");
                            fp.WriteLine($"unk0               = [{data1[i].data4[j].value:x8}]");
                            fp.WriteLine("");
                        }
                    }

                    fp.WriteLine("\n");
                    fp.WriteLine("data5\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data5.Length; j++)
                        {

                            fp.WriteLine($"***** {i:x4}({i}) *****");
                            fp.WriteLine($"real address       = [{mStNIFL.mNIFL.rel0_offset + data1[i].fltd_data5_addr + j * NGS.fltd_data5.GetMyDataSize():x8}]\n");
                            fp.WriteLine($"value0             = [{data1[i].data5[j].value0:x8}]");
                            fp.WriteLine($"reserve0           = [{data1[i].data5[j].reserve0:x8}]");
                            fp.WriteLine($"value1             = [{data1[i].data5[j].value1:x8}]");
                            fp.WriteLine($"idk0               = [{data1[i].data5[j].idk0:x8}]");
                            fp.WriteLine($"idk1               = [{data1[i].data5[j].idk1:x8}]");
                            fp.WriteLine($"idk2               = [{data1[i].data5[j].idk2:x8}]");
                            fp.WriteLine($"idk3               = [{data1[i].data5[j].idk3:x8}]");
                            fp.WriteLine($"data7 count        = [{data1[i].data5[j].count_addr0:x8}]");
                            fp.WriteLine($"data8 count        = [{data1[i].data5[j].count_addr1:x8}]");
                            fp.WriteLine($"data9 count        = [{data1[i].data5[j].count_addr2:x8}]");
                            fp.WriteLine($"data10 count       = [{data1[i].data5[j].count_addr3:x8}]");
                            fp.WriteLine($"data7 offset       = [{data1[i].data5[j].fltd_data7_addr:x8}]");
                            fp.WriteLine($"reserve            = [{data1[i].data5[j].reserve1:x8}]");
                            fp.WriteLine($"reserve            = [{data1[i].data5[j].reserve2:x8}]");
                            fp.WriteLine($"data8 offset       = [{data1[i].data5[j].fltd_data8_addr:x8}]");
                            fp.WriteLine($"data9 offset       = [{data1[i].data5[j].fltd_data9_addr:x8}]");
                            fp.WriteLine($"data10 offset      = [{data1[i].data5[j].fltd_data10_addr:x8}]");
                            fp.WriteLine($"address            = [{data1[i].data5[j].address4:x8}]");
                            fp.WriteLine($"reserve            = [{data1[i].data5[j].reserve3:x8}]");
                            fp.WriteLine($"data12 offset      = [{data1[i].data5[j].fltd_data12_addr:x8}]");
                            fp.WriteLine("");
                        }
                    }
                    fp.WriteLine("\n");
                    fp.WriteLine("data6\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        fp.WriteLine($"***** {i:x4}({i}) *****");
                        fp.WriteLine($"real address       = [{mStNIFL.mNIFL.rel0_offset + data1[i].fltd_data6_addr:x8}]\n");
                        fp.WriteLine($"reserve           = [{data1[i].data6.reserve:x8}]");
                        fp.WriteLine($"value0            = [{data1[i].data6.value0:x8}]");
                        fp.WriteLine($"value1            = [{data1[i].data6.value1:x8}]");
                        fp.WriteLine("");
                    }
                    fp.WriteLine("\n");
                    fp.WriteLine("data7\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data5.Length; j++)
                        {
                            for (int k = 0; k < data1[i].data5[j].data7.Length; k++)
                            {
                                fp.WriteLine($"***** {k:x4}({k}) *****");
                                fp.WriteLine($"real address       = [{mStNIFL.mNIFL.rel0_offset + data1[i].data5[j].fltd_data7_addr + j * NGS.fltd_data7.GetMyDataSize():x8}]\n");
                                fp.WriteLine($"value0             = [{data1[i].data5[j].data7[k].value:x8}]");
                                fp.WriteLine("");
                            }
                        }
                    }
                    fp.WriteLine("\n");
                    fp.WriteLine("data8\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data5.Length; j++)
                        {
                            for (int k = 0; k < data1[i].data5[j].data8.Length; k++)
                            {

                                fp.WriteLine($"***** {k:x4}({k}) *****");
                                fp.WriteLine($"real address     = [{mStNIFL.mNIFL.rel0_offset + data1[i].data5[j].fltd_data8_addr + j * NGS.fltd_data8.GetMyDataSize():x8}]\n");
                                fp.WriteLine($"idk0             = [{data1[i].data5[j].data8[k].idk0:x8}]");
                                fp.WriteLine($"idk1             = [{data1[i].data5[j].data8[k].idk1:x8}]");
                                fp.WriteLine($"idk2             = [{data1[i].data5[j].data8[k].idk2:x8}]");
                                fp.WriteLine($"idk3             = [{data1[i].data5[j].data8[k].idk3:x8}]");
                                fp.WriteLine($"reserve          = [{data1[i].data5[j].data8[k].reserve0:x8}]");
                                fp.WriteLine($"reserve          = [{data1[i].data5[j].data8[k].reserve1:x8}]");
                                fp.WriteLine($"reserve          = [{data1[i].data5[j].data8[k].reserve2:x8}]");
                                fp.WriteLine($"value            = [{data1[i].data5[j].data8[k].value0:x8}]");
                                fp.WriteLine($"value            = [{data1[i].data5[j].data8[k].value1:x8}]");
                                fp.WriteLine("");
                            }
                        }
                    }
                    fp.WriteLine("\n");
                    fp.WriteLine("data9\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data5.Length; j++)
                        {
                            for (int k = 0; k < data1[i].data5[j].data9.Length; k++)
                            {
                                fp.WriteLine($"***** {k:x4}({k}) *****");
                                fp.WriteLine($"real address     = [{mStNIFL.mNIFL.rel0_offset + data1[i].data5[j].fltd_data9_addr + j * NGS.fltd_data9.GetMyDataSize():x8}]\n");
                                fp.WriteLine($"idk0             = [{data1[i].data5[j].data9[k].idk0:x8}]");
                                fp.WriteLine($"idk1             = [{data1[i].data5[j].data9[k].idk1:x8}]");
                                fp.WriteLine($"idk2             = [{data1[i].data5[j].data9[k].idk2:x8}]");
                                fp.WriteLine($"idk3             = [{data1[i].data5[j].data9[k].idk3:x8}]");
                                fp.WriteLine($"reserve          = [{data1[i].data5[j].data9[k].reserve0:x8}]");
                                fp.WriteLine($"reserve          = [{data1[i].data5[j].data9[k].reserve1:x8}]");
                                fp.WriteLine($"value            = [{data1[i].data5[j].data9[k].value0:x8}]");
                                fp.WriteLine($"value            = [{data1[i].data5[j].data9[k].value1:x8}]");
                                fp.WriteLine($"value            = [{data1[i].data5[j].data9[k].value2:x8}]");
                                fp.WriteLine("");
                            }
                        }
                    }
                    fp.WriteLine("\n");
                    fp.WriteLine("data10\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data5.Length; j++)
                        {
                            for (int k = 0; k < data1[i].data5[j].data10.Length; k++)
                            {
                                fp.WriteLine($"***** {k:x4}({k}) *****");
                                fp.WriteLine($"real address       = [{mStNIFL.mNIFL.rel0_offset + data1[i].data5[j].fltd_data10_addr + j * NGS.fltd_data10.GetMyDataSize():x8}]\n");
                                fp.WriteLine($"idk0             = [{data1[i].data5[j].data10[k].idk0:x8}]");
                                fp.WriteLine($"idk1             = [{data1[i].data5[j].data10[k].idk1:x8}]");
                                fp.WriteLine($"idk2             = [{data1[i].data5[j].data10[k].idk2:x8}]");
                                fp.WriteLine($"idk3             = [{data1[i].data5[j].data10[k].idk3:x8}]");
                                fp.WriteLine($"reserve          = [{data1[i].data5[j].data10[k].reserve0:x8}]");
                                fp.WriteLine($"reserve          = [{data1[i].data5[j].data10[k].reserve1:x8}]");
                                fp.WriteLine($"reserve          = [{data1[i].data5[j].data10[k].reserve2:x8}]");
                                fp.WriteLine($"value            = [{data1[i].data5[j].data10[k].value0:x8}]");
                                fp.WriteLine("");
                            }
                        }
                    }
                    fp.WriteLine("\n");
                    fp.WriteLine("data11\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data5.Length; j++)
                        {
                            {
                                fp.WriteLine($"real address       = [{mStNIFL.mNIFL.rel0_offset + data1[i].data5[j].fltd_data11_addr:x8}]\n");
                                fp.WriteLine($"value            = [{data1[i].data5[j].data11.value0:x8}]");
                                fp.WriteLine($"value            = [{data1[i].data5[j].data11.value1:x8}]");
                                fp.WriteLine("");
                            }
                        }
                    }
                    fp.WriteLine("\n");
                    fp.WriteLine("data12\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data5.Length; j++)
                        {
                            fp.WriteLine($"***** {j:x4}({j}) *****");
                            fp.WriteLine($"real address       = [{mStNIFL.mNIFL.rel0_offset + data1[i].data5[j].fltd_data12_addr:x8}]\n");
                            fp.WriteLine($"data13 offset          = [{data1[i].data5[j].data12.fltd_data13_addr:x8}]");
                            fp.WriteLine($"data14 offset          = [{data1[i].data5[j].data12.fltd_data14_addr:x8}]");
                            fp.WriteLine($"reserve          = [{data1[i].data5[j].data12.reserve:x8}]");
                            fp.WriteLine($"value            = [{data1[i].data5[j].data12.value:x8}]");
                            fp.WriteLine("");

                        }
                    }
                    fp.WriteLine("\n");
                    fp.WriteLine("data13\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data5.Length; j++)
                        {
                            for (int k = 0; k < data1[i].data5[j].data12.data13.Length; k++)
                            {
                                fp.WriteLine($"***** {k:x4}({k}) *****");
                                fp.WriteLine($"real address       = [{mStNIFL.mNIFL.rel0_offset + data1[i].data5[j].data12.fltd_data13_addr + k * NGS.fltd_data13.GetMyDataSize():x8}]\n");
                                fp.WriteLine($"value0             =");
                                for (int l = 0; l < 0x5; l++)
                                {
                                    fp.WriteLine($"{data1[i].data5[j].data12.data13[k].value0[l]}");
                                }
                                fp.WriteLine("");
                                fp.WriteLine($"value            = [{data1[i].data5[j].data12.data13[k].value1:x8}]");
                                fp.WriteLine($"value            = [{data1[i].data5[j].data12.data13[k].value2:x8}]");
                                fp.WriteLine($"value            = [{data1[i].data5[j].data12.data13[k].value3:x8}]");
                                fp.WriteLine("");
                            }

                        }
                    }
                    fp.WriteLine("\n");
                    fp.WriteLine("data14\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data5.Length; j++)
                        {
                            for (int k = 0; k < data1[i].data5[j].data12.data14.Length; k++)
                            {
                                fp.WriteLine($"***** {k:x4}({k}) *****");
                                fp.WriteLine($"real address       = [{mStNIFL.mNIFL.rel0_offset + data1[i].data5[j].data12.fltd_data14_addr + k * NGS.fltd_data14.GetMyDataSize():x8}]\n");
                                fp.WriteLine($"reserve          = [{data1[i].data5[j].data12.data14[k].reserve0:x8}]");
                                fp.WriteLine($"value            = [{data1[i].data5[j].data12.data14[k].value0:x8}]");
                                fp.WriteLine($"reserve          = [{data1[i].data5[j].data12.data14[k].reserve0:x8}]");
                                fp.WriteLine($"reserve          = [{data1[i].data5[j].data12.data14[k].reserve1:x8}]");
                                fp.WriteLine($"value            = [{data1[i].data5[j].data12.data14[k].value1:x8}]");
                                fp.WriteLine("");
                            }
                        }
                    }
                }
                else
                {

                }
            }
            return true;
        }
    }
}
