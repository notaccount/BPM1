
//闭包限定命名空间
(function ($) {
    $.extend({
    	//只针对左右两栏布局 且左栏overflow不能为hidden
        hasngSplitbar: function (options) {
        	//默认参数
		    var defaluts = {
		        eleLeft: '', //左边区域id
		        eleRight: '', //右边区域id
		        toggleRelative:[0,0], //上下 左右  非空 相对定位供toggle位置错开方便点击
		        disableToggle:true, //是否禁止图标toggle
		        disableDrag:false, //是否禁止拖动
		        onDragBefore:function(){}, //拖动前回调
		        onDragAfter:function(){}, //拖动后回调
		        onMoveLeftEnd:function(){},//点击向左滑动回调
		        onMoveRightEnd:function(){}//点击向右滑动回调
		    };
            var opts = $.extend({}, defaluts, options); //使用jQuery.extend 覆盖插件默认参数
            
            var ele_left=$(opts.eleLeft);
			var ele_right=$(opts.eleRight);

			var has_style = $('style#hasng_splitbar').length>0?true:false;
			if (!has_style) {
				$('head').append('<style id="hasng_splitbar">'+
									'.hasng-splitbar-parent:hover .hasng-splitbar-toogleicon{width:10px;}'+
									'.hasng-splitbar.disableToggle .hasng-splitbar-toogleicon{display:none;}'+
									'.hasng-splitbar.disableDrag{width:1px; border-left:none; background-color:transparent; cursor:default;}'+
									'.hasng-splitbar.disableDrag:hover{border-left:none;}'+
									'.hssplit-contentwrap{height:100%; overflow:hidden;}'+
									'.hasng-splitbar{position:absolute; top:0; left:100%; bottom:0; width:4px; border-left:1px solid #cecece; cursor:col-resize;}'+
									'.hasng-splitbar:hover{border-left:1px solid #41a7e5;}'+
									'.hasng-splitbar-toogleicon{position:absolute; top:50%; left:100%; margin-top:-35px; height:70px; width:0px; overflow:hidden; opacity:0.8; border:1px solid #ccc; border-left:none; border-radius:0 10px 10px 0; background-color:#f6f6f6; cursor:pointer;}'+
									'.hasng-splitbar-parent .hasng-splitbar-toogleicon:hover{width:20px; opacity:1;}'+
									'.hasng-splitbar-toogleicon:hover .iconfont{color:#41a7e5;}'+
									'.hasng-splitbar-toogleicon .iconfont{position:absolute; top:50%; left:50%; margin-top:-8px; margin-left:-8px; font-size:14px; color:#777;}'+
									'.hssplit-layer{position:absolute; top:0; right:0; bottom:0; left:0;}'+
								'</style>');
			};

			var theme='';
			if (opts.disableToggle) {theme += ' disableToggle';};
			if (opts.disableDrag) {theme += ' disableDrag';};

			ele_left.addClass('hasng-splitbar-parent').append('<div class="hasng-splitbar'+theme+'"><div class="hasng-splitbar-toogleicon"><i class="icon iconfont">&#xe660;</i></div></div>');
			var ele_rightPosition = ele_right.css('position');
			if (ele_rightPosition!=='relative'&&ele_rightPosition!='absolute'&&ele_rightPosition!='fixed') {
				ele_right.css({'position':'relative'});
			};


			var splitbar = ele_left.find('.hasng-splitbar').eq(0);
			var splitbar_toggleicon = ele_left.find('.hasng-splitbar-toogleicon').eq(0);
			var left_width = ele_left.width();
			var right_initialWidthToLeft = parseFloat(ele_right.css('margin-left').replace('px'))-left_width;

			splitbar_toggleicon_top = parseFloat(splitbar_toggleicon.css('top').replace('px'));
			splitbar_toggleicon_left = parseFloat(splitbar_toggleicon.css('left').replace('px'));
			splitbar_toggleicon.css({'top':splitbar_toggleicon_top+opts.toggleRelative[0],'left':splitbar_toggleicon_left+opts.toggleRelative[1]});

			//禁止文字选中
			/*if(document.all){
			    document.onselectstart= function(){return false;}; //for ie
			}else{
			    document.onmousedown= function(){return false;};
			    document.onmouseup= function(){return true;};
			}
			document.onselectstart = new Function('event.returnValue=false;');*/

			if (!opts.disableDrag) {

				(function(){
					var is_move=false;
					splitbar.mousedown(function(){
						opts.onDragBefore();
						is_move=true;
						//防止右容器中含iframe导致不触发当前页面的mousemove事件
						ele_right.append('<div class="hssplit-layer"></div>');
					});
					splitbar.mouseup(function(){
						is_move=false;
						$('.hssplit-layer').remove();
						opts.onDragAfter();
						
					});

					$(document).mousemove(function(event){
						if (is_move) {
							var mouseX_now = event.clientX-ele_left.offset().left-splitbar.width()/2;
							mouseX_now = mouseX_now<=0?0:mouseX_now;
							if (mouseX_now===0) {is_move=false};
							ele_left.css({'width':mouseX_now});
							//ele_left.find('.hs-contentwrap').css({'width':'100%'});
							ele_right.css({'margin-left':mouseX_now+splitbar.width()});
							left_width = ele_left.width();
							if (left_width) {
								splitbar_toggleicon.html('<i class="icon iconfont">&#xe660;</i>');
							}else{
								//splitbar_toggleicon.html('<i class="icon iconfont">&#xe65f;</i>');
								ele_right.css({'margin-left':0});
								opts.onDragAfter();
//								mini.alert("设置成功")
								
							};
						};
						
					});
					
				})();
			};



			if (!opts.disableToggle) {
				splitbar_toggleicon.click(function(event){
					
					//如果已收起
					if (ele_left.width()) {
						ele_left.animate({'width':0},300,function(){
							setTimeout(function(){
								ele_right.animate({"margin-left":0},100,function(){
									opts.onMoveLeftEnd();
								});
								
								
							},0)
						});
						ele_left.find('.slidemenu').animate({'width':0},300);
						ele_right.animate({'margin-left':right_initialWidthToLeft},300);
						splitbar_toggleicon.html('<i class="icon iconfont">&#xe65f;</i>');
					}else{
						ele_left.animate({'width':left_width},300,function(){
//							setTimeout(function(){
								opts.onMoveRightEnd();
//							},0)
						})
						ele_left.find('.slidemenu').animate({'width':'100%'},300);
						ele_right.animate({'margin-left':left_width+right_initialWidthToLeft},300);
						splitbar_toggleicon.html('<i class="icon iconfont">&#xe660;</i>');
					};
				}).mousedown(function(event){
					event.stopPropagation();
				}).mouseup(function(event){
					event.stopPropagation();
				});
			};
		}
    });

	
})(window.jQuery);
