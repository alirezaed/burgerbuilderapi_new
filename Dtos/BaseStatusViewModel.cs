﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BurgerBuilder.WebApi.ViewModels
{
	public abstract class BaseStatusViewModel
	{
		public string message { get; set; }
		public bool status { get; set; }
	}
}