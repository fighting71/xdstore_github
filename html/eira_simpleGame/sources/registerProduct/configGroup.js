/**
 * 配置信息数组
 */
var configArr = new Array();

$(document).ready(function() {

	var $selItem;

	$(".edit_main .btn_save").click(function() {//保存配置信息
		var info = $("#edit_main").val();//获取录入信息
		var configID = $("#edit_main").prop("name");//获取录入编号
		if(info.trim().length > 0 && configID) {//为空验证
			var data = {//封装结果集
				key: configID,
				value: info
			};
			configArr.push(data);//将添加项追加到配置数组中
			$("#edit_main").val("");//清空录入
			$("#edit_main").prop("name", "");//清空记录
			$(".edit_configList .item_list").append($selItem.removeClass("selConfig"));//将添加项追移动到已添加列表中

			$selItem.unbind("click");//取消绑定原有click事件
			$selItem.click(function() {//重新绑定click事件
				var $ele = $(this);
				reback($ele, data)
			});

			console.log(configArr);
		}
		$(".edit_main").hide();
	});

	function taget_eidt($ele) {//添加配置信息
		$ele.siblings(".selConfig").removeClass("selConfig");//唯一编辑
		$selItem = $ele;
		$selItem.addClass("selConfig");
		var id = $selItem.attr("data_id");//读取配置信息
		var description = $selItem.text();//读取配置信息

		$(".edit_main .description").text(description);//加载配置信息
		$("#edit_main").prop("name", id);//加载配置信息
		$(".edit_main").show();//显示编辑区域
	}

	$(".configList .item_list>li").click(function() {
		var $ele = $(this);
		taget_eidt($ele);
	});

	function reback($ele, info) {
		$(".configList .item_list").append($ele); //回归元素
		//移除信息
		var index = $.inArray(info, configArr); //查找删除元素的index
		configArr.splice(index, 1);//元素移除并重新排序
		console.log(configArr);
		//重新绑定事件
		$ele.unbind("click");
		$ele.click(function() {
			taget_eidt($ele);
		});
	}
});