﻿using Domain.Common.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Common
{
    public class ApiResult
    {
        private readonly List<string> _successes;
        private readonly List<string> _errors;
        public ApiResult()
        {
            _successes = new List<string>();
            _errors = new List<string>();
            IsSuccess = true;
        }
        public bool IsSuccess { get; private set; }
        public ApiResultStatusCode? Status { get; set; }
        public IReadOnlyList<string> Successes => _successes;
        public IReadOnlyList<string> Errors => _errors;

        public void AddError(string message)
        {
            message = message.CleanString();
            if(message == null)
                return;
            if(_errors.Contains(message))
                return;
            _errors.Add(message);
            IsSuccess = false;
        }
        public void AddErrors(IEnumerable<string> messages)
        {
            foreach (var message in messages)
            {
                AddError(message);
            }
        }

        public void RemoveError(string message)
        {
            message = message.CleanString();
            if (message == null)
                return;
            _errors.Remove(message);
            if (!_errors.Any())
                IsSuccess = true;
        }

        public void CleareErrorMessages()
        {
            _errors.Clear();
        }
        public void AddSuccess(string message)
        {
            message = message.CleanString();
            if (message == null)
                return;
            if (_successes.Contains(message))
                return;
            _successes.Add(message);
        }
        public void AddSuccesses(IEnumerable<string> messages)
        {
            foreach (var message in messages)
            {
                _successes.Add(message);
            }
        }
        public void RemoveSuccess(string message)
        {
            message = message.CleanString();
            if (message == null)
                return;
            _successes.Remove(message);
        }

        public void CleareSuccessMessages()
        {
            _successes.Clear();
        }
    }

    public class ApiResult<TData> : ApiResult
        where TData : class
    {
        public TData Data { get; set; }
   

        public void Success(TData data)
        {
            Status = ApiResultStatusCode.Success;
            Data = data;
            AddSuccess(ApiResultStatusCode.Success.ToDisplay());
        }
    }

    
}
