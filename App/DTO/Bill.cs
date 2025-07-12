using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DTO
{
    public class Bill
    {

        public Bill(int id, DateTime? dateCheckIn, DateTime? dateCheckOut, int status)
        {
            this.Id = id;
            this.DateCheckIn = dateCheckIn;
            this.DateCheckOut = dateCheckOut;
            this.Status = status;
        }

        public Bill(DataRow row)
        {
            this.Id = (int)row["idBill"];
            this.DateCheckIn = row["dateCheckIn"] == DBNull.Value ? null : (DateTime?)row["dateCheckIn"];
            this.DateCheckOut = row["dateCheckOut"] == DBNull.Value ? null : (DateTime?)row["dateCheckOut"];
            this.Status = (int)row["status"];
        }
        public int status;
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        private DateTime? dateCheckIn;
        public DateTime? DateCheckIn
        {
            get { return dateCheckIn; }
            set { dateCheckIn = value; }
        }

        private DateTime? dateCheckOut;
        public DateTime? DateCheckOut
        {
            get { return dateCheckOut; }
            set { dateCheckOut = value; }
        }

        private int iD;
        public int Id
        {
            get { return iD; }
            set { iD = value; }
        }

    }
}
