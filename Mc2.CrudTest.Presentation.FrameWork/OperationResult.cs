using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.FrameWork
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string TableName { get; set; }
        public string Operation { get; set; }
        public DateTime OperationDate { get; set; }
        public int RecordId { get; set; }

        public OperationResult(string operation , string tableName)
        {
            this.Operation = operation;
            this.TableName = tableName;
            this.Success = true;
            this.OperationDate = DateTime.Now;
        }

        public OperationResult(string operation, string tableName , int recordId)
        {
            this.Operation = operation;
            this.TableName = tableName;
            this.RecordId = recordId;
            this.Success = true;
            this.OperationDate = DateTime.Now;
        }

        public OperationResult ToSuccess(string message)
        {
            this.Message = message;
            this.Success = true;
            return this;
        }

        public OperationResult ToSuccess(string message , int recordId)
        {
            this.Message = message;
            this.RecordId = recordId;
            this.Success = true;
            return this;
        }

        public OperationResult ToFail(string message)
        {
            this.Message = message;
            this.Success = false;
            return this;
        }

        public OperationResult ToFail(string message, int recordId)
        {
            this.Message = message;
            this.RecordId = recordId;
            this.Success = false;
            return this;
        }
    }
}
