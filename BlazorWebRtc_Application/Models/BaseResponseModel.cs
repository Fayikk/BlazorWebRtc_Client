﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebRtc_Application.Models
{
    public class BaseResponseModel
    {
        public string Message { get; set; } 
        public bool isSuccess { get; set; }
        public object Data { get; set; }    
    }
}
