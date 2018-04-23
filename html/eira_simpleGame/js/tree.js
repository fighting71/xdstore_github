$('.tree .tree_node .title').click(function() {
			var $showNode = $(this).parents('.tree').find('.showNode');
			//折叠其他
			$showNode.removeClass('showNode');
			
			var level = $(this).parents('ul').attr('level');
			//			console.log(level);//根据级别计算间距
			if($(this).next().length == 0){
				//无子级》》进行跳转
			}
			$(this).next().toggle();
//			$(this).next().css('text-indent', '+' + level + 'em');
			$(this).addClass('showNode');
		});
		
		$('.tree ul>.tree_node').click(function(){
//			var index = $(this).index();
//			console.log(index);
			$(this).prevAll().find('.sub_node').hide();
			$(this).nextAll().find('.sub_node').hide();
		});