
//var ApiHelper = /** @class */ (function() {
//	function ApiHelper() {}

	/**
	 * 参数加签
	 * @param {Object} paramInfo 参数信息  
	 * @param {Number} optMethod 操作标识码
//	 * @param {String} dbName
	 * @param {String} description 描述信息
	 * @param {String} cmdID 平台
	 * 
	 */
	function api_SignParm(paramInfo, optMethod, description,cmdID) {
		//解析为json字符串
		var data_str = JSON.stringify(paramInfo);

		//	console.log("参数信息:" + data_str);

		//参数集封装
		var data_json = {
			OptMethod: optMethod,
//			DbName: dbName,
			ParamObj: data_str,
			Description:description,
			CmdID:cmdID
		};

		//获取byte数组并排序
		var bytes = stringToByte(data_str).sort(sortNumber);
		//	console.log("获取byte数组并排序:" + bytes);

		//获取排序后的字符串
		var sort_str = byteToString(bytes);
		//	console.log("获取排序后的字符串:" + sort_str);

		//参数加签
		data_json.Sign = $.md5(sort_str);

		//console.log("本地签名:" + data_json.Sign);

		//将参数信息转换为json字符串
		var param_str = JSON.stringify(data_json);

		return param_str;

	}

	/**
	 * 发送api请求
	 * @param {Object} paramInfo 参数信息  
	 * @param {Number} optMethod 操作标识码
//	 * @param {String} dbName
	 * @param {String} description 描述信息
	 * @param {String} cmdID 平台
	 * @param {Function} dealfunc 处理方法
	 */
	function api_SendRequest(paramInfo, optMethod, description,cmdID, dealfunc) {
		var data_str = SignParm(paramInfo, optMethod, description,cmdID);
		$.ajax({
			type: "post",
			url: "http://XD.Store.Api.ImgServer/XdStore/Index",
			async: true,
			dataType: "json",
			data: data_str,
			success: dealfunc,
			error: function() {
				alert("error>>>");
			}
		});
	}

	/**
	 * string>>byte[]
	 * @param {String} str
	 */
	function stringToByte(str) {
		var bytes = new Array();
		var len, c;
		len = str.length;
		for(var i = 0; i < len; i++) {
			c = str.charCodeAt(i);
			if(c >= 0x010000 && c <= 0x10FFFF) {
				bytes.push(((c >> 18) & 0x07) | 0xF0);
				bytes.push(((c >> 12) & 0x3F) | 0x80);
				bytes.push(((c >> 6) & 0x3F) | 0x80);
				bytes.push((c & 0x3F) | 0x80);
			} else if(c >= 0x000800 && c <= 0x00FFFF) {
				bytes.push(((c >> 12) & 0x0F) | 0xE0);
				bytes.push(((c >> 6) & 0x3F) | 0x80);
				bytes.push((c & 0x3F) | 0x80);
			} else if(c >= 0x000080 && c <= 0x0007FF) {
				bytes.push(((c >> 6) & 0x1F) | 0xC0);
				bytes.push((c & 0x3F) | 0x80);
			} else {
				bytes.push(c & 0xFF);
			}
		}
		return bytes;

	}

	/**
	 * byte[] >> string
	 * @param {Array} arr
	 */
	function byteToString(arr) {
		if(typeof arr === 'string') {
			return arr;
		}
		var str = '',
			_arr = arr;
		for(var i = 0; i < _arr.length; i++) {
			var one = _arr[i].toString(2),
				v = one.match(/^1+?(?=0)/);
			if(v && one.length == 8) {
				var bytesLength = v[0].length;
				var store = _arr[i].toString(2).slice(7 - bytesLength);
				for(var st = 1; st < bytesLength; st++) {
					store += _arr[st + i].toString(2).slice(2);
				}
				str += String.fromCharCode(parseInt(store, 2));
				i += bytesLength - 1;
			} else {
				str += String.fromCharCode(_arr[i]);
			}
		}
		return str;
	}

	/**
	 * 根据数字大小进行排序
	 * @param {Number} a
	 * @param {Number} b
	 */
	function sortNumber(a, b) {
		return a - b
	}

//}());