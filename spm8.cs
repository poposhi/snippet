public class SPM8
{
    private ushort address_v = 4107; //讀取v資料的起點
    private ushort address_power_a = 4135; //讀取功率資料的起點
    private ushort address_energy = 4185; //讀取能量資料的起點
    private ushort address_PF = 4159; //讀取PF資料的起點
    public byte slaveID = 77; //
    public string deviceID = "77"; //
    public bool CommFailureFlag = false;
    private string Fault_info = "77";

    private static ushort[] data0 = new ushort[125];
    private static ushort[] data = new ushort[24];
    private static ushort[] data2 = new ushort[24];
    private static ushort[] data3 = new ushort[8];

    #region 定義變數 
    private double v_a = 77;
    private double v_b = 77;
    private double v_c = 77;
    public double v = 77;
    private double vl_ab = 77;
    private double vl_bc = 77;
    private double vl_ca = 77;
    private double vl = 77;
    private double i_a = 77;
    private double i_b = 77;
    private double i_c = 77;
    public double i = 77;
    private double i_n = 77;
    private double power_a = 77; //
    private double power_b = 77; //
    private double power_c = 77; //
    public double power_total = 77; //三相功率 
    private double Q1 = 77; //
    private double Q2 = 77; //
    private double Q3 = 77; //
    public double Q_total = 77; //
    public double exp_wh_h = 77; //
    private double imp_wh_h = 77; //
    private double imp_varh_h = 77; // 	消耗  輸入實功電能
    private double exp_varh_h = 77; //提供虛功  輸出實功電能
    private double vah_h = 77;
    private double tot_wh_h = 77;
    private double net_wh_h = 77;
    private double tot_varh_h = 77;
    private double net_varh_h = 77;
    private double s_a = 77;
    private double s_b = 77;
    private double s_c = 77;
    private double s = 77;

    public double f = 77;
    private double pf_a = 77;
    private double pf_b = 77;
    private double pf_c = 77;
    private double pf = 77;



    #endregion
    public void udate_to_db(IMongoDatabase db, string Collection)
    {
        try
        {
            DateTime now = DateTime.Now;
            var coll = db.GetCollection<BsonDocument>(Collection);  //指定寫入給"categories"此collection  
            coll.InsertOne(new BsonDocument { { "time", now.AddHours(Grid_Control.time_offset)}, { "ID", deviceID},
                {"f",this.f},
                {"v",this.v},
                {"vl",this.vl},
                {"i",this.i},
                {"i_n",this.i_n},
                {"v_a",this.v_a},
                {"v_b",this.v_b},
                {"v_c",this.v_c},
                {"vl_ab",this.vl_ab},
                {"vl_bc",this.vl_bc},
                {"vl_ca",this.vl_ca},
                {"i_a",this.i_a},
                {"i_b",this.i_b},
                {"i_c",this.i_c},
                {"p_a",this.power_a},
                {"p_b",this.power_b},
                {"p_c",this.power_c},
                {"q_a",this.Q1},
                {"q_b",this.Q2},
                {"q_c",this.Q3},
                {"p",this.power_total },
                {"q",this.Q_total },
                {"s_a",this.s_a},
                {"s_b",this.s_b},
                {"s_c",this.s_c},
                {"s",this.s},
                {"pf_a",this.pf_a},
                {"pf_b",this.pf_b},
                {"pf_c",this.pf_c},
                {"pf",this.pf},
                {"imp_wh_h",this.imp_wh_h},
                {"exp_wh_h",this.exp_wh_h},
                {"tot_wh_h",this.tot_wh_h},
                {"net_wh_h",this.net_wh_h},
                {"imp_varh_h",this.imp_varh_h},
                {"exp_varh_h",this.exp_varh_h},
                {"tot_varh_h",this.tot_varh_h},
                {"net_varh_h",this.net_varh_h},
                {"vah_h",this.vah_h},

                });
        }
        catch (Exception e)
        {
            Console.WriteLine("spm8 db Error");
            Debug.Print("spm8 db Error");
            Debug.Print(e.Message);
            //
        }
    }


    public bool Read_pq_kwh(ref ModbusIpMaster master) //tcp使用
    {
        if (master != null)
        {
            try
            {
                Thread.Sleep(Form1.WaitingTime);
                //ushort[] data0 = new ushort[28];
                data0 = master.ReadInputRegisters(this.slaveID, address_v, 28);
                v_a = ushortToFloat(data0[0], data0[1]);
                v_b = ushortToFloat(data0[2], data0[3]);
                v_c = ushortToFloat(data0[4], data0[5]);
                v = ushortToFloat(data0[6], data0[7]);
                vl_ab = ushortToFloat(data0[8], data0[9]);
                vl_bc = ushortToFloat(data0[10], data0[11]);
                vl_ca = ushortToFloat(data0[12], data0[13]);
                vl = ushortToFloat(data0[14], data0[15]);
                i_a = ushortToFloat(data0[16], data0[17]);
                i_b = ushortToFloat(data0[18], data0[19]);
                i_c = ushortToFloat(data0[20], data0[21]);
                i = ushortToFloat(data0[22], data0[23]);
                i_n = ushortToFloat(data0[24], data0[25]);
                f = ushortToFloat(data0[26], data0[27]);

                Thread.Sleep(Form1.WaitingTime);
                data = master.ReadInputRegisters(this.slaveID, address_power_a, 24);
                power_a = ushortToFloat(data[0], data[1]);
                power_b = ushortToFloat(data[2], data[3]);
                power_c = ushortToFloat(data[4], data[5]);
                power_total = ushortToFloat(data[6], data[7]);
                Q1 = ushortToFloat(data[8], data[9]);
                Q2 = ushortToFloat(data[10], data[12]);
                Q3 = ushortToFloat(data[12], data[13]);
                Q_total = ushortToFloat(data[14], data[15]);
                s_a = ushortToFloat(data[16], data[17]);
                s_b = ushortToFloat(data[18], data[19]);
                s_c = ushortToFloat(data[20], data[21]);
                s = ushortToFloat(data[22], data[23]);
                Thread.Sleep(Form1.WaitingTime);
                data2 = master.ReadInputRegisters(slaveID, address_energy, 18);

                exp_wh_h = ushortToFloat(data2[0], data2[1]);
                imp_wh_h = ushortToFloat(data2[2], data2[3]);
                tot_wh_h = ushortToFloat(data2[4], data2[5]);
                net_wh_h = ushortToFloat(data2[6], data2[7]);
                exp_varh_h = ushortToFloat(data2[10], data2[11]);
                imp_varh_h = ushortToFloat(data2[8], data2[9]);
                tot_varh_h = ushortToFloat(data2[12], data2[13]);
                net_varh_h = ushortToFloat(data2[14], data2[15]);
                vah_h = ushortToFloat(data2[16], data2[17]);

                Thread.Sleep(Form1.WaitingTime);

                data3 = master.ReadInputRegisters(slaveID, address_PF, 8);
                pf_a = ushortToFloat(data3[0], data3[1]);
                pf_b = ushortToFloat(data3[2], data3[3]);
                pf_c = ushortToFloat(data3[4], data3[5]);
                pf = ushortToFloat(data3[6], data3[7]);

                CommFailureFlag = false;
                Fault_info = "Communication is normal";
                return true;
            }
            catch (Exception e)
            {

                CommFailureFlag = true;
                Fault_info = e.Message;
                return false;
            }
        }
        return false;
    }
    public void Read_pq_kwh(ModbusSerialMaster master)
    {
        try
        {
            Thread.Sleep(Form1.WaitingTime);
            ushort[] data0 = new ushort[28];
            data0 = master.ReadInputRegisters(this.slaveID, address_v, 28);
            v_a = ushortToFloat(data0[0], data0[1]);
            v_b = ushortToFloat(data0[2], data0[3]);
            v_c = ushortToFloat(data0[4], data0[5]);
            v = ushortToFloat(data0[6], data0[7]);
            vl_ab = ushortToFloat(data0[8], data0[9]);
            vl_bc = ushortToFloat(data0[10], data0[11]);
            vl_ca = ushortToFloat(data0[12], data0[13]);
            vl = ushortToFloat(data0[14], data0[15]);
            i_a = ushortToFloat(data0[16], data0[17]);
            i_b = ushortToFloat(data0[18], data0[19]);
            i_c = ushortToFloat(data0[20], data0[21]);
            i = ushortToFloat(data0[22], data0[23]);
            i_n = ushortToFloat(data0[24], data0[25]);
            f = ushortToFloat(data0[26], data0[27]);

            Thread.Sleep(Form1.WaitingTime);
            ushort[] data = new ushort[24];
            data = master.ReadInputRegisters(this.slaveID, address_power_a, 24);
            power_a = ushortToFloat(data[0], data[1]);
            power_b = ushortToFloat(data[2], data[3]);
            power_c = ushortToFloat(data[4], data[5]);
            power_total = ushortToFloat(data[6], data[7]);
            Q1 = ushortToFloat(data[8], data[9]);
            Q2 = ushortToFloat(data[10], data[12]);
            Q3 = ushortToFloat(data[12], data[13]);
            Q_total = ushortToFloat(data[14], data[15]);
            s_a = ushortToFloat(data[16], data[17]);
            s_b = ushortToFloat(data[18], data[19]);
            s_c = ushortToFloat(data[20], data[21]);
            s = ushortToFloat(data[22], data[23]);
            Thread.Sleep(Form1.WaitingTime);
            ushort[] data2 = new ushort[24];
            data2 = master.ReadInputRegisters(slaveID, address_energy, 18);
            exp_wh_h = ushortToFloat(data2[0], data2[1]);
            imp_wh_h = ushortToFloat(data2[2], data2[3]);
            tot_wh_h = ushortToFloat(data2[4], data2[5]);
            net_wh_h = ushortToFloat(data2[6], data2[7]);
            imp_varh_h = ushortToFloat(data2[10], data2[11]);
            exp_varh_h = ushortToFloat(data2[8], data2[9]);
            tot_varh_h = ushortToFloat(data2[12], data2[13]);
            net_varh_h = ushortToFloat(data2[14], data2[15]);
            vah_h = ushortToFloat(data2[16], data2[17]);

            Thread.Sleep(Form1.WaitingTime);
            ushort[] data3 = new ushort[8];
            data3 = master.ReadInputRegisters(slaveID, address_PF, 8);
            pf_a = ushortToFloat(data3[0], data3[1]);
            pf_b = ushortToFloat(data3[2], data3[3]);
            pf_c = ushortToFloat(data3[4], data3[5]);
            pf = ushortToFloat(data3[6], data3[7]);

            CommFailureFlag = false;
            Fault_info = "Communication is normal";
        }
        catch (Exception e)
        {
            CommFailureFlag = true;
            Fault_info = e.Message;
        }

    }
    public void Read_impotent(ModbusSerialMaster master)
    {
        ushort[] data0 = new ushort[50];
        data0 = master.ReadInputRegisters(this.slaveID, address_v, 36);
        v_a = ushortToFloat(data0[0], data0[1]);
        v_b = ushortToFloat(data0[2], data0[3]);
        v_c = ushortToFloat(data0[4], data0[5]);
        v = ushortToFloat(data0[6], data0[7]);
        vl_ab = ushortToFloat(data0[8], data0[9]);
        vl_bc = ushortToFloat(data0[10], data0[11]);
        vl_ca = ushortToFloat(data0[12], data0[13]);
        vl = ushortToFloat(data0[14], data0[15]);
        i_a = ushortToFloat(data0[16], data0[17]);
        i_b = ushortToFloat(data0[18], data0[19]);
        i_c = ushortToFloat(data0[20], data0[21]);
        i = ushortToFloat(data0[22], data0[23]);
        i_n = ushortToFloat(data0[24], data0[25]);
        f = ushortToFloat(data0[26], data0[27]);
        power_a = ushortToFloat(data0[28], data0[29]);
        power_b = ushortToFloat(data0[30], data0[31]);
        power_c = ushortToFloat(data0[32], data0[33]);
        power_total = ushortToFloat(data0[34], data0[35]);
    }
    private double ushortToFloat(ushort left, ushort right)
    {
        //要先轉換成二進制
        string binary1 = Convert.ToString(left, 2).PadLeft(16, '0');
        string binary2 = Convert.ToString(right, 2).PadLeft(16, '0');
        string b3 = binary1 + binary2;
        //再轉換成 浮點數 
        int i = Convert.ToInt32(b3, 2);
        byte[] b = BitConverter.GetBytes(i);

        return Math.Round(BitConverter.ToSingle(b, 0), 2);
    }
}