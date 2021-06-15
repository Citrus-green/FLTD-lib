using System.IO;

namespace FLTD_lib
{
    public partial class FLTD
    {
        public bool DumpData(string filePath)
        {
            using (var fp = new StreamWriter(new FileStream(filePath, FileMode.Create, FileAccess.Write)))
            {
                fp.WriteLine("**** NIFL ****\n");
                fp.WriteLine($"real address         = [0]\n");
                fp.WriteLine($"tag                  = [{mStNIFL.mNIFL.tag:X8}]");
                fp.WriteLine($"reserve0             = [{mStNIFL.mNIFL.reserve0:X8}]");
                fp.WriteLine($"reserve1             = [{mStNIFL.mNIFL.reserve1:X8}]");
                fp.WriteLine($"rel0_offset          = [{mStNIFL.mNIFL.rel0_offset:X8}]");
                fp.WriteLine($"rel0_size            = [{mStNIFL.mNIFL.rel0_size:X8}]");
                fp.WriteLine($"nof0_offset          = [{mStNIFL.mNIFL.nof0_offset:X8}]");
                fp.WriteLine($"nof0_size            = [{mStNIFL.mNIFL.nof0_size:X8}]");
                fp.WriteLine($"reserve0             = [{mStNIFL.mNIFL.reserve2:X8}]");
                fp.WriteLine("");

                fp.WriteLine("**** REL0 ****\n");
                fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset:X8}]\n");
                fp.WriteLine($"tag                  = [{mStNIFL.mREL0.tag:X8}]");
                fp.WriteLine($"size                 = [{mStNIFL.mREL0.size:X8}]");
                fp.WriteLine($"entry                = [{mStNIFL.mREL0.entry:X8}]");
                fp.WriteLine($"reserve              = [{mStNIFL.mREL0.reserve:X8}]");
                fp.WriteLine("");

                fp.WriteLine("**** NOF0 ****\n");
                fp.WriteLine($"real address         = [{mStNIFL.mNIFL.nof0_offset:X8}]\n");
                fp.WriteLine($"tag                  = [{mStNIFL.mNOF0.tag:X8}]");
                fp.WriteLine($"size                 = [{mStNIFL.mNOF0.size:X8}]");
                fp.WriteLine($"count                = [{mStNIFL.mNOF0.count:X8}]");
                fp.WriteLine("\nNOF0 Address List");
                uint[] addresses = mStNIFL.mNOF0.GetOriginalAddresses(mStNIFL.mNIFL.rel0_offset);
                for (int i = 0; i < addresses.Length; i++)
                {
                    fp.WriteLine($"{i:X4}  [{mStNIFL.mNOF0.addresses[i]:X8}] => [{addresses[i]:X8}] [{ mStNIFL.mNOF0.orgAddresses[i]:X8}]");
                }
                fp.WriteLine("");

                fp.WriteLine("**** FLTD ****\n");
                fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset + mStNIFL.mREL0.entry:X8}]\n");
                fp.WriteLine($"format               = [{data_array.format:X8}]");
                fp.WriteLine($"data0 count          = [{data_array.count_addr0:X8}]");
                fp.WriteLine($"data1 count          = [{data_array.count_addr1:X8}]");
                fp.WriteLine($"idk                  = [{data_array.idk3:X8}]");
                fp.WriteLine($"data0 offset         = [{data_array.fltd_data0_addr:X8}]");
                fp.WriteLine($"data1 offset         = [{data_array.fltd_data1_addr:X8}]");
                fp.WriteLine($"reserve              = [{data_array.reserve:X8}]");
                fp.WriteLine($"data2 offset         = [{data_array.fltd_data2_addr:X8}]");
                fp.WriteLine("");

                if (data_array.IsNGS() == true)
                {
                    NGS.fltd_data0[] data0 = (NGS.fltd_data0[])data_array.data0;
                    NGS.fltd_data1[] data1 = (NGS.fltd_data1[])data_array.data1;
                    NGS.fltd_data2 data2 = (NGS.fltd_data2)data_array.data2;

                    fp.WriteLine("**** data0 ****\n");
                    for (int i = 0; i < data0.Length; i++)
                    {
                        fp.WriteLine($"***** {i:X4}({i}) *****");
                        fp.WriteLine("data0\n");
                        fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset + data_array.fltd_data0_addr + i * NGS.fltd_data0.GetMyDataSize():X8}]\n");
                        fp.WriteLine($"index                = [{data0[i].index:X8}]");
                        fp.WriteLine($"name address count   = [{data0[i].count_addr0:X8}]");
                        fp.WriteLine($"data3 count          = [{data0[i].count_addr1:X8}]");
                        fp.WriteLine($"reserve              = [{data0[i].reserve:X8}]");
                        fp.WriteLine($"name address offset  = [{data0[i].name_address_address:X8}]");
                        fp.WriteLine($"data3 offset         = [{data0[i].fltd_data3_addr:X8}]");
                        fp.WriteLine($"value0               = [{data0[i].value0:X8}]");
                        fp.WriteLine($"value1               = [{data0[i].value1:X8}]");
                        fp.WriteLine("\naddress and name");
                        for (int j = 0; j < data0[i].count_addr0; j++)
                        {
                            fp.WriteLine($"[{data0[i].nameAddressList[j]:X8}]{data0[i].nameList[j]}");
                        }
                        fp.WriteLine("");
                    }

                    fp.WriteLine("**** data1 ****\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        fp.WriteLine($"***** {i:X4}({i}) *****");
                        fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset + data_array.fltd_data1_addr + i * NGS.fltd_data1.GetMyDataSize():X8}]\n");
                        fp.WriteLine($"idk0                 = [{data1[i].idk0:X8}]");
                        fp.WriteLine($"data13 count         = [{data1[i].count_fltd_data13:X8}]");
                        fp.WriteLine($"idk2                 = [{data1[i].count_addr1:X8}]");
                        fp.WriteLine($"idk3                 = [{data1[i].idk3:X8}]");
                        fp.WriteLine($"data4 count          = [{data1[i].count_addr0:X8}]");
                        fp.WriteLine($"data5 count          = [{data1[i].count_addr1:X8}]");
                        fp.WriteLine($"idk6                 = [{data1[i].idk6:X8}]");
                        fp.WriteLine($"idk7                 = [{data1[i].idk7:X8}]");
                        fp.WriteLine($"data4 offset         = [{data1[i].fltd_data4_addr:X8}]");
                        fp.WriteLine($"data5 offset         = [{data1[i].fltd_data5_addr:X8}]");
                        fp.WriteLine($"data6 offset         = [{data1[i].fltd_data6_addr:X8}]");
                        fp.WriteLine($"value0               = [{data1[i].value0:X8}]");
                        fp.WriteLine($"value1               = [{data1[i].value1:X8}]");
                        fp.WriteLine("");
                    }
                    fp.WriteLine("");

                    fp.WriteLine("**** data2 ****\n");
                    fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset + data_array.fltd_data2_addr:X8}]\n");
                    fp.WriteLine($"value0               = [{data2.value0:X8}]");
                    fp.WriteLine($"fin address?         = [{data2.address0:X8}]");
                    fp.WriteLine($"value1               = [{data2.value1:X8}]");
                    fp.WriteLine($"value2               = [{data2.value2:X8}]");

                    fp.WriteLine("");

                    fp.WriteLine("**** data3 ****\n");
                    for (int h = 0; h < data0.Length; h++)
                    {
                        for (int i = 0; i < data0[h].data3.Length; i++)
                        {
                            fp.WriteLine($"***** {i:X4}({i}) *****");
                            fp.WriteLine($"real address       = [{mStNIFL.mNIFL.rel0_offset + data0[h].fltd_data3_addr + i * NGS.fltd_data3.GetMyDataSize():X8}]\n");
                            fp.WriteLine($"unk0               = [{data0[h].data3[i].idk0:X8}]");
                            fp.WriteLine($"unk1               = [{data0[h].data3[i].idk1:X8}]");
                            fp.WriteLine($"unk2               = [{data0[h].data3[i].idk2:X8}]");
                            fp.WriteLine($"unk3               = [{data0[h].data3[i].idk3:X8}]");
                            fp.WriteLine($"value0             =");
                            for (int k = 0; k < 0xe; k++)
                            {
                                fp.WriteLine($"{data0[h].data3[i].value0[k]}");
                            }
                            fp.WriteLine("");
                            fp.WriteLine($"reserve0           = [{data0[h].data3[i].reserve0:X8}]");
                            fp.WriteLine($"reserve1           = [{data0[h].data3[i].reserve1:X8}]");

                            fp.WriteLine($"name offset        = [{data0[h].data3[i].name_address0:X8}]({data0[h].data3[i].name0})");
                            fp.WriteLine($"address1           = [{data0[h].data3[i].address1:X8}]");
                            fp.WriteLine($"address2           = [{data0[h].data3[i].address2:X8}]");
                            fp.WriteLine($"name_offset_offset = [{data0[h].data3[i].name_address1:X8}]({data0[h].data3[i].name1})");

                            fp.WriteLine($"value5             = [{data0[h].data3[i].value5:X8}]");
                            fp.WriteLine($"value6             = [{data0[h].data3[i].value6:X8}]");
                            fp.WriteLine("");
                        }
                    }

                    fp.WriteLine("");
                    fp.WriteLine("**** data4 ****\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data4.Length; j++)
                        {
                            fp.WriteLine($"***** {i:X4}({i}) *****");
                            fp.WriteLine($"real address       = [{mStNIFL.mNIFL.rel0_offset + data1[i].fltd_data4_addr + j * NGS.fltd_data4.GetMyDataSize():X8}]\n");
                            fp.WriteLine($"unk0               = [{data1[i].data4[j].value:X8}]");
                            fp.WriteLine("");
                        }
                    }

                    fp.WriteLine("");
                    fp.WriteLine("**** data5 ****\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data5.Length; j++)
                        {

                            fp.WriteLine($"***** {i:X4}({i}) *****");
                            fp.WriteLine($"real address       = [{mStNIFL.mNIFL.rel0_offset + data1[i].fltd_data5_addr + j * NGS.fltd_data5.GetMyDataSize():X8}]\n");
                            fp.WriteLine($"value0             = [{data1[i].data5[j].value0:X8}]");
                            fp.WriteLine($"reserve0           = [{data1[i].data5[j].reserve0:X8}]");
                            fp.WriteLine($"value1             = [{data1[i].data5[j].value1:X8}]");
                            fp.WriteLine($"idk0               = [{data1[i].data5[j].idk0:X8}]");
                            fp.WriteLine($"idk1               = [{data1[i].data5[j].idk1:X8}]");
                            fp.WriteLine($"idk2               = [{data1[i].data5[j].idk2:X8}]");
                            fp.WriteLine($"idk3               = [{data1[i].data5[j].idk3:X8}]");
                            fp.WriteLine($"data7 count        = [{data1[i].data5[j].count_addr0:X8}]");
                            fp.WriteLine($"data8 count        = [{data1[i].data5[j].count_addr1:X8}]");
                            fp.WriteLine($"data9 count        = [{data1[i].data5[j].count_addr2:X8}]");
                            fp.WriteLine($"data10 count       = [{data1[i].data5[j].count_addr3:X8}]");
                            fp.WriteLine($"data7 offset       = [{data1[i].data5[j].fltd_data7_addr:X8}]");
                            fp.WriteLine($"reserve            = [{data1[i].data5[j].reserve1:X8}]");
                            fp.WriteLine($"reserve            = [{data1[i].data5[j].reserve2:X8}]");
                            fp.WriteLine($"data8 offset       = [{data1[i].data5[j].fltd_data8_addr:X8}]");
                            fp.WriteLine($"data9 offset       = [{data1[i].data5[j].fltd_data9_addr:X8}]");
                            fp.WriteLine($"data10 offset      = [{data1[i].data5[j].fltd_data10_addr:X8}]");
                            fp.WriteLine($"address            = [{data1[i].data5[j].address4:X8}]");
                            fp.WriteLine($"data11 offset      = [{data1[i].data5[j].fltd_data11_addr:X8}]");
                            fp.WriteLine($"reserve            = [{data1[i].data5[j].reserve3:X8}]");
                            fp.WriteLine($"data12 offset      = [{data1[i].data5[j].fltd_data12_addr:X8}]");
                            fp.WriteLine("");
                        }
                    }
                    fp.WriteLine("");
                    fp.WriteLine("**** data6 ****\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        fp.WriteLine($"***** {i:X4}({i}) *****");
                        fp.WriteLine($"real address       = [{mStNIFL.mNIFL.rel0_offset + data1[i].fltd_data6_addr:X8}]\n");
                        fp.WriteLine($"reserve            = [{data1[i].data6.reserve:X8}]");
                        fp.WriteLine($"value0             = [{data1[i].data6.value0:X8}]");
                        fp.WriteLine($"value1             = [{data1[i].data6.value1:X8}]");
                        fp.WriteLine("");
                    }
                    fp.WriteLine("");
                    fp.WriteLine("**** data7 ****\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data5.Length; j++)
                        {
                            for (int k = 0; k < data1[i].data5[j].data7.Length; k++)
                            {
                                fp.WriteLine($"***** {k:X4}({k}) *****");
                                fp.WriteLine($"real address       = [{mStNIFL.mNIFL.rel0_offset + data1[i].data5[j].fltd_data7_addr + k * NGS.fltd_data7.GetMyDataSize():X8}]\n");
                                fp.WriteLine($"value0             = [{data1[i].data5[j].data7[k].value:X8}]");
                                fp.WriteLine("");
                            }
                        }
                    }
                    fp.WriteLine("");
                    fp.WriteLine("**** data8 ****\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data5.Length; j++)
                        {
                            for (int k = 0; k < data1[i].data5[j].data8.Length; k++)
                            {

                                fp.WriteLine($"***** {k:X4}({k}) *****");
                                fp.WriteLine($"real address       = [{mStNIFL.mNIFL.rel0_offset + data1[i].data5[j].fltd_data8_addr + k * NGS.fltd_data8.GetMyDataSize():X8}]\n");
                                fp.WriteLine($"idk0               = [{data1[i].data5[j].data8[k].idk0:X8}]");
                                fp.WriteLine($"idk1               = [{data1[i].data5[j].data8[k].idk1:X8}]");
                                fp.WriteLine($"idk2               = [{data1[i].data5[j].data8[k].idk2:X8}]");
                                fp.WriteLine($"idk3               = [{data1[i].data5[j].data8[k].idk3:X8}]");
                                fp.WriteLine($"reserve            = [{data1[i].data5[j].data8[k].reserve0:X8}]");
                                fp.WriteLine($"reserve            = [{data1[i].data5[j].data8[k].reserve1:X8}]");
                                fp.WriteLine($"value              = [{data1[i].data5[j].data8[k].value2:X8}]");
                                fp.WriteLine($"value              = [{data1[i].data5[j].data8[k].value0:X8}]");
                                fp.WriteLine($"value              = [{data1[i].data5[j].data8[k].value1:X8}]");
                                fp.WriteLine("");
                            }
                        }
                    }
                    fp.WriteLine("");
                    fp.WriteLine("**** data9 ****\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data5.Length; j++)
                        {
                            for (int k = 0; k < data1[i].data5[j].data9.Length; k++)
                            {
                                fp.WriteLine($"***** {k:X4}({k}) *****");
                                fp.WriteLine($"real address       = [{mStNIFL.mNIFL.rel0_offset + data1[i].data5[j].fltd_data9_addr + k * NGS.fltd_data9.GetMyDataSize():X8}]\n");
                                fp.WriteLine($"idk0               = [{data1[i].data5[j].data9[k].idk0:X8}]");
                                fp.WriteLine($"idk1               = [{data1[i].data5[j].data9[k].idk1:X8}]");
                                fp.WriteLine($"idk2               = [{data1[i].data5[j].data9[k].idk2:X8}]");
                                fp.WriteLine($"idk3               = [{data1[i].data5[j].data9[k].idk3:X8}]");
                                fp.WriteLine($"reserve            = [{data1[i].data5[j].data9[k].reserve0:X8}]");
                                fp.WriteLine($"reserve            = [{data1[i].data5[j].data9[k].reserve1:X8}]");
                                fp.WriteLine($"value              = [{data1[i].data5[j].data9[k].value0:X8}]");
                                fp.WriteLine($"value              = [{data1[i].data5[j].data9[k].value1:X8}]");
                                fp.WriteLine($"value              = [{data1[i].data5[j].data9[k].value2:X8}]");
                                fp.WriteLine("");
                            }
                        }
                    }
                    fp.WriteLine("");
                    fp.WriteLine("**** data10 ****\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data5.Length; j++)
                        {
                            for (int k = 0; k < data1[i].data5[j].data10.Length; k++)
                            {
                                fp.WriteLine($"***** {k:X4}({k}) *****");
                                fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset + data1[i].data5[j].fltd_data10_addr + j * NGS.fltd_data10.GetMyDataSize():X8}]\n");
                                fp.WriteLine($"idk0                 = [{data1[i].data5[j].data10[k].idk0:X8}]");
                                fp.WriteLine($"idk1                 = [{data1[i].data5[j].data10[k].idk1:X8}]");
                                fp.WriteLine($"idk2                 = [{data1[i].data5[j].data10[k].idk2:X8}]");
                                fp.WriteLine($"idk3                 = [{data1[i].data5[j].data10[k].idk3:X8}]");
                                fp.WriteLine($"reserve              = [{data1[i].data5[j].data10[k].reserve0:X8}]");
                                fp.WriteLine($"reserve              = [{data1[i].data5[j].data10[k].reserve1:X8}]");
                                fp.WriteLine($"reserve              = [{data1[i].data5[j].data10[k].reserve2:X8}]");
                                fp.WriteLine($"value                = [{data1[i].data5[j].data10[k].value0:X8}]");
                                fp.WriteLine("");
                            }
                        }
                    }
                    fp.WriteLine("");
                    fp.WriteLine("**** data11 ****\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data5.Length; j++)
                        {
                            {
                                fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset + data1[i].data5[j].fltd_data11_addr:X8}]\n");
                                fp.WriteLine($"value                = [{data1[i].data5[j].data11.value0:X8}]");
                                fp.WriteLine($"value                = [{data1[i].data5[j].data11.value1:X8}]");
                                fp.WriteLine("");
                            }
                        }
                    }
                    fp.WriteLine("");
                    fp.WriteLine("**** data12 ****\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data5.Length; j++)
                        {
                            fp.WriteLine($"***** {j:X4}({j}) *****");
                            fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset + data1[i].data5[j].fltd_data12_addr:X8}]\n");
                            fp.WriteLine($"data13 offset        = [{data1[i].data5[j].data12.fltd_data13_addr:X8}]");
                            fp.WriteLine($"data14 offset        = [{data1[i].data5[j].data12.fltd_data14_addr:X8}]");
                            fp.WriteLine($"reserve              = [{data1[i].data5[j].data12.reserve:X8}]");
                            fp.WriteLine($"value                = [{data1[i].data5[j].data12.value:X8}]");
                            fp.WriteLine("");

                        }
                    }
                    fp.WriteLine("");
                    fp.WriteLine("**** data13 ****\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data5.Length; j++)
                        {
                            for (int k = 0; k < data1[i].data5[j].data12.data13.Length; k++)
                            {
                                fp.WriteLine($"***** {k:X4}({k}) *****");
                                fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset + data1[i].data5[j].data12.fltd_data13_addr + k * NGS.fltd_data13.GetMyDataSize():X8}]\n");
                                fp.WriteLine($"value0               =");
                                for (int l = 0; l < 0x5; l++)
                                {
                                    fp.WriteLine($"{data1[i].data5[j].data12.data13[k].value0[l]}");
                                }
                                fp.WriteLine("");
                                fp.WriteLine($"value                = [{data1[i].data5[j].data12.data13[k].value1:X8}]");
                                fp.WriteLine($"value                = [{data1[i].data5[j].data12.data13[k].value2:X8}]");
                                fp.WriteLine($"value                = [{data1[i].data5[j].data12.data13[k].value3:X8}]");
                                fp.WriteLine("");
                            }

                        }
                    }
                    fp.WriteLine("");
                    fp.WriteLine("**** data14 ****\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data5.Length; j++)
                        {
                            for (int k = 0; k < data1[i].data5[j].data12.data14.Length; k++)
                            {
                                fp.WriteLine($"***** {k:X4}({k}) *****");
                                fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset + data1[i].data5[j].data12.fltd_data14_addr + k * NGS.fltd_data14.GetMyDataSize():X8}]\n");
                                fp.WriteLine($"value                = [{data1[i].data5[j].data12.data14[k].value0:X8}]");
                                fp.WriteLine($"value                = [{data1[i].data5[j].data12.data14[k].value1:X8}]");
                                fp.WriteLine($"value                = [{data1[i].data5[j].data12.data14[k].value2:X8}]");
                                fp.WriteLine($"reserve              = [{data1[i].data5[j].data12.data14[k].reserve2:X8}]");
                                fp.WriteLine($"value                = [{data1[i].data5[j].data12.data14[k].value3:X8}]");
                                fp.WriteLine("");
                            }
                        }
                    }
                }
                else
                {
                    Classic.fltd_data0[] data0 = (Classic.fltd_data0[])data_array.data0;
                    Classic.fltd_data1[] data1 = (Classic.fltd_data1[])data_array.data1;
                    Classic.fltd_data2 data2 = (Classic.fltd_data2)data_array.data2;

                    fp.WriteLine("**** data0 ****\n");
                    for (int i = 0; i < data0.Length; i++)
                    {
                        fp.WriteLine($"***** {i:X4}({i}) *****");
                        fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset + data_array.fltd_data0_addr + i * Classic.fltd_data0.GetMyDataSize():X8}]\n");
                        fp.WriteLine($"index                = [{data0[i].index:X8}]");
                        fp.WriteLine($"name address count   = [{data0[i].count_addr0:X8}]");
                        fp.WriteLine($"data3 count          = [{data0[i].count_addr1:X8}]");
                        fp.WriteLine($"reserve              = [{data0[i].reserve:X8}]");
                        fp.WriteLine($"name address offset  = [{data0[i].name_address_address:X8}]");
                        fp.WriteLine($"data3 offset         = [{data0[i].fltd_data3_addr:X8}]");
                        fp.WriteLine($"value0               = [{data0[i].value0:X8}]");
                        fp.WriteLine($"address              = [{data0[i].address:X8}]");
                        fp.WriteLine("\naddress and name");
                        for (int j = 0; j < data0[i].count_addr0; j++)
                        {
                            fp.WriteLine($"[{data0[i].nameAddressList[j]:X8}]{data0[i].nameList[j]}");
                        }
                        fp.WriteLine("");
                    }
					fp.WriteLine("**** data1 ****\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        fp.WriteLine($"***** {i:X4}({i}) *****");
                        fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset + data_array.fltd_data1_addr + i * Classic.fltd_data0.GetMyDataSize():X8}]\n");
                        fp.WriteLine($"index                = [{data1[i].idk0:X8}]");
                        fp.WriteLine($"name address count   = [{data1[i].data10_count:X8}]");
                        fp.WriteLine($"idk2                 = [{data1[i].idk2:X8}]");
                        fp.WriteLine($"idk3                 = [{data1[i].idk3:X8}]");
                        fp.WriteLine($"count_addr0          = [{data1[i].count_addr0:X8}]");
                        fp.WriteLine($"count_addr1          = [{data1[i].count_addr1:X8}]");
                        fp.WriteLine($"count_addr2          = [{data1[i].count_addr2:X8}]");
                        fp.WriteLine($"idk7                 = [{data1[i].idk7:X8}]");
                        fp.WriteLine($"fltd_data4_addr      = [{data1[i].fltd_data4_addr:X8}]");
                        fp.WriteLine($"fltd_data5_addr      = [{data1[i].fltd_data5_addr:X8}]");
                        fp.WriteLine($"fltd_data6_addr      = [{data1[i].fltd_data6_addr:X8}]");
                        fp.WriteLine($"value0               = [{data1[i].value0:X8}]");
                        fp.WriteLine($"address              = [{data1[i].address:X8}]");
                        fp.WriteLine("");
                    }
					fp.WriteLine("**** data2 ****\n");
					fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset + data_array.fltd_data2_addr:X8}]\n");
					fp.WriteLine($"value0               = [{data2.value0:X8}]");
					fp.WriteLine("");
					
                    fp.WriteLine("**** data3 ****\n");
                    for (int i = 0; i < data0.Length; i++)
                    {
						for (int j = 0; j < data0[i].data3.Length; j++)
						{
							fp.WriteLine($"***** {j:X4}({j}) *****");
							fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset + data0[i].fltd_data3_addr + j * Classic.fltd_data3.GetMyDataSize():X8}]\n");
							fp.WriteLine($"idk0                 = [{data0[i].data3[j].idk0:X8}]");
							fp.WriteLine($"idk1                 = [{data0[i].data3[j].idk1:X8}]");
							fp.WriteLine($"idk2                 = [{data0[i].data3[j].idk2:X8}]");
							fp.WriteLine($"idk3                 = [{data0[i].data3[j].idk3:X8}]");
							fp.WriteLine($"value0               =");
							for (int k=0;k<data0[i].data3[j].value0.Length;k++)
								fp.WriteLine($"{data0[i].data3[j].value0[k]}");
							fp.WriteLine($"name_offset          = [{data0[i].data3[j].name_address0:X8}]({data0[i].data3[j].name})");
							fp.WriteLine($"address1             = [{data0[i].data3[j].address1:X8}]");
							fp.WriteLine($"address2             = [{data0[i].data3[j].address2:X8}]");
							fp.WriteLine("");
						}
                    }
                    fp.WriteLine("**** data4 ****\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
						for (int j = 0; j < data1[i].data4.Length; j++)
						{
							fp.WriteLine($"***** {j:X4}({j}) *****");
							fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset + data1[i].fltd_data4_addr + j * Classic.fltd_data4.GetMyDataSize():X8}]\n");
							fp.WriteLine($"idk0                 = [{data1[i].data4[j].value0:X8}]");
							fp.WriteLine($"idk1                 = [{data1[i].data4[j].value1:X8}]");
							fp.WriteLine($"idk2                 = [{data1[i].data4[j].value2:X8}]");
							fp.WriteLine($"idk3                 = [{data1[i].data4[j].idk0:X8}]");
							fp.WriteLine($"count0               = [{data1[i].data4[j].count0:X8}]");
							fp.WriteLine($"count1               = [{data1[i].data4[j].count1:X8}]");
							fp.WriteLine($"count2               = [{data1[i].data4[j].count2:X8}]");
							fp.WriteLine($"count3               = [{data1[i].data4[j].count3:X8}]");
							fp.WriteLine($"address0             = [{data1[i].data4[j].address0:X8}]");
							fp.WriteLine($"reserve0             = [{data1[i].data4[j].reserve0:X8}]");
							fp.WriteLine($"reserve1             = [{data1[i].data4[j].reserve1:X8}]");
							fp.WriteLine($"addrdata6 offset     = [{data1[i].data4[j].fltd_data6_addr:X8}]");
							fp.WriteLine($"address2             = [{data1[i].data4[j].address2:X8}]");
							fp.WriteLine($"data7 offset         = [{data1[i].data4[j].fltd_data7_addr:X8}]");
							fp.WriteLine($"address4             = [{data1[i].data4[j].address4:X8}]");
							fp.WriteLine($"data8 offset         = [{data1[i].data4[j].fltd_data8_addr:X8}]");
							fp.WriteLine($"reserve2             = [{data1[i].data4[j].reserve2:X8}]");
							fp.WriteLine($"data9 offset         = [{data1[i].data4[j].fltd_data9_addr:X8}]");
							fp.WriteLine("");
						}
                    }
                    fp.WriteLine("**** data5 ****\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data5.Length; j++)
                        {
                            fp.WriteLine($"***** {j:X4}({j}) *****");
                            fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset + data1[i].fltd_data5_addr + j * Classic.fltd_data5.GetMyDataSize():X8}]\n");
                            fp.WriteLine($"reserve              = [{data1[i].data5[j].reserve:X8}]");
                            fp.WriteLine($"value0               = [{data1[i].data5[j].value0:X8}]");
                            fp.WriteLine($"address0             = [{data1[i].data5[j].address0:X8}]");
                            fp.WriteLine($"value1               = [{data1[i].data5[j].value1:X8}]");
                            fp.WriteLine("");
                        }
                    }
                    fp.WriteLine("**** data6 ****\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data4.Length; j++)
                        {
                            for (int k = 0; k < data1[i].data4[j].data6.Length; k++)
                            {
                                fp.WriteLine($"***** {k:X4}({k}) *****");
                                fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset + data1[i].data4[j].fltd_data6_addr + k* Classic.fltd_data6.GetMyDataSize():X8}]\n");
                                fp.WriteLine($"value0               = [{data1[i].data4[j].data6[k].value0:X8}]");
                                fp.WriteLine($"reserve0             = [{data1[i].data4[j].data6[k].reserve0:X8}]");
                                fp.WriteLine($"reserve0             = [{data1[i].data4[j].data6[k].reserve1:X8}]");
                                fp.WriteLine($"reserve0             = [{data1[i].data4[j].data6[k].reserve2:X8}]");
                                fp.WriteLine($"reserve0             = [{data1[i].data4[j].data6[k].reserve3:X8}]");
                                fp.WriteLine($"value1               = [{data1[i].data4[j].data6[k].value1:X8}]");
                                fp.WriteLine("");
                            }
                        }
                    }
                    fp.WriteLine("**** data7 ****\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data4.Length; j++)
                        {
                            for (int k = 0; k < data1[i].data4[j].data7.Length; k++)
                            {
                                fp.WriteLine($"***** {k:X4}({k}) *****");
                                fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset + data1[i].data4[j].fltd_data7_addr + k * Classic.fltd_data7.GetMyDataSize():X8}]\n");
                                fp.WriteLine($"idk0                 = [{data1[i].data4[j].data7[k].idk0:X8}]");
                                fp.WriteLine($"idk1                 = [{data1[i].data4[j].data7[k].idk1:X8}]");
                                fp.WriteLine($"idk2                 = [{data1[i].data4[j].data7[k].idk2:X8}]");
                                fp.WriteLine($"idk3                 = [{data1[i].data4[j].data7[k].idk3:X8}]");
                                fp.WriteLine($"reserve0             = [{data1[i].data4[j].data7[k].reserve0:X8}]");
                                fp.WriteLine($"reserve1             = [{data1[i].data4[j].data7[k].reserve1:X8}]");
                                fp.WriteLine($"reserve2             = [{data1[i].data4[j].data7[k].reserve2:X8}]");
                                fp.WriteLine($"value0               = [{data1[i].data4[j].data7[k].value0:X8}]");
                                fp.WriteLine("");
                            }
                        }
                    }
                    fp.WriteLine("**** data8 ****\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data4.Length; j++)
                        {
                            for (int k = 0; k < data1[i].data4[j].data8.Length; k++)
                            {
                                fp.WriteLine($"***** {k:X4}({k}) *****");
                                fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset + data1[i].data4[j].fltd_data8_addr + k * Classic.fltd_data8.GetMyDataSize():X8}]\n");
                                fp.WriteLine($"value0               = [{data1[i].data4[j].data8[k].value0:X8}]");
                                fp.WriteLine($"value1               = [{data1[i].data4[j].data8[k].value1:X8}]");
                                fp.WriteLine("");
                            }
                        }
                    }
                    fp.WriteLine("**** data9 ****\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data4.Length; j++)
                        {
                            for (int k = 0; k < data1[i].data4[j].data9.Length; k++)
                            {
                                fp.WriteLine($"***** {k:X4}({k}) *****");
                                fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset + data1[i].data4[j].fltd_data9_addr + k * Classic.fltd_data9.GetMyDataSize():X8}]\n");
                                fp.WriteLine($"fltd_data11_offset   = [{data1[i].data4[j].data9[k].fltd_data10_addr:X8}]");
                                fp.WriteLine($"fltd_data11_offset   = [{data1[i].data4[j].data9[k].fltd_data11_addr:X8}]");
                                fp.WriteLine($"reserve              = [{data1[i].data4[j].data9[k].reserve:X8}]");
                                fp.WriteLine($"value0               = [{data1[i].data4[j].data9[k].value0:X8}]");
                                fp.WriteLine("");
                            }
                        }
                    }
                    fp.WriteLine("**** data10 ****\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data4.Length; j++)
                        {
                            for (int k = 0; k < data1[i].data4[j].data9.Length; k++)
                            {
                                for (int l = 0; l < data1[i].data4[j].data9[k].data10.Length; l++)
                                {
                                    fp.WriteLine($"***** {l:X4}({l}) *****");
                                    fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset + data1[i].data4[j].data9[k].fltd_data10_addr + l * Classic.fltd_data10.GetMyDataSize():X8}]\n");
                                    fp.WriteLine($"value0               = [{data1[i].data4[j].data9[k].data10[l].value0:X8}]");
                                    fp.WriteLine($"value1               = [{data1[i].data4[j].data9[k].data10[l].value1:X8}]");
                                    fp.WriteLine($"value2               = [{data1[i].data4[j].data9[k].data10[l].value2:X8}]");
                                    fp.WriteLine($"value3               = [{data1[i].data4[j].data9[k].data10[l].value3:X8}]");
                                    fp.WriteLine($"value4               = [{data1[i].data4[j].data9[k].data10[l].value4:X8}]");
                                    fp.WriteLine($"value5               = [{data1[i].data4[j].data9[k].data10[l].value5:X8}]");
                                    fp.WriteLine($"value6               = [{data1[i].data4[j].data9[k].data10[l].value6:X8}]");
                                    fp.WriteLine($"value7               = [{data1[i].data4[j].data9[k].data10[l].value7:X8}]");
                                    fp.WriteLine("");
                                }
                            }
                        }
                    }
                    fp.WriteLine("**** data11 ****\n");
                    for (int i = 0; i < data1.Length; i++)
                    {
                        for (int j = 0; j < data1[i].data4.Length; j++)
                        {
                            for (int k = 0; k < data1[i].data4[j].data9.Length; k++)
                            {
                                for (int l = 0; l < data1[i].data4[j].data9[k].data11.Length; l++)
                                {
                                    fp.WriteLine($"***** {l:X4}({l}) *****");
                                    fp.WriteLine($"real address         = [{mStNIFL.mNIFL.rel0_offset + data1[i].data4[j].data9[k].fltd_data11_addr + l * Classic.fltd_data11.GetMyDataSize():X8}]\n");
                                    fp.WriteLine($"value0               = [{data1[i].data4[j].data9[k].data11[l].value0:X8}]");
                                    fp.WriteLine($"value1               = [{data1[i].data4[j].data9[k].data11[l].value1:X8}]");
                                    fp.WriteLine($"value2               = [{data1[i].data4[j].data9[k].data11[l].value2:X8}]");
                                    fp.WriteLine($"value3               = [{data1[i].data4[j].data9[k].data11[l].value3:X8}]");
                                    fp.WriteLine($"value4               = [{data1[i].data4[j].data9[k].data11[l].value4:X8}]");
                                    fp.WriteLine("");
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}
