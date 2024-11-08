﻿using System;
using System.Threading;

namespace TuyaLink.Communication
{
    public class ResponseHandler(string messageId, bool acknowledgment)
    {

        public static ResponseHandler FromResponse(FunctionResponse result)
        {
            return new ResponseHandler(Guid.NewGuid().To32String(), true)
            {
                _report = result
            };
        }
        private readonly bool _acknowledgment = acknowledgment;
        private FunctionResponse? _report;

        protected AutoResetEvent? ResetEvent { get; private set; } = acknowledgment ? new AutoResetEvent(false) : null;
        public string MessageId { get; } = messageId;

        private void CheckAknowlage()
        {
            if (!_acknowledgment)
            {
                throw new InvalidOperationException("Response does not provide acknowledgment");
            }
        }

        public virtual FunctionResponse WaitForAcknowledgeReport(int millisecondsTimeout = Timeout.Infinite, bool exitContext = false)
        {
            CheckAknowlage();
            if (_report != null)
            {
                return _report;
            }
            ResetEvent!.WaitOne(millisecondsTimeout, exitContext);
            if (_report == null)
            {
                throw new TimeoutException("Timeout waiting for acknowledgment");
            }
            return _report;
        }

        internal void Acknowledge(FunctionResponse report)
        {
            if (report.MsgId != MessageId)
            {
                throw new ArgumentException("MessageId does not match");
            }
            if (!_acknowledgment || ResetEvent is null)
            {
                return;
            }
            _report = report;
            ResetEvent.Set();
        }
    }
}
