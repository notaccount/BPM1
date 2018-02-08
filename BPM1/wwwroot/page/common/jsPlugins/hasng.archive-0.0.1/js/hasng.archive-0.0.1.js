
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
		        disableToggle:false, //是否禁止图标toggle
		        disableDrag:false, //是否禁止拖动
		        onDragBefore:function(){}, //拖动前回调
		        onDragAfter:function(){} //拖动后回调
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
							};
						};
					});
				})();
			};



			if (!opts.disableToggle) {
				splitbar_toggleicon.click(function(event){
					
					//如果已收起
					if (ele_left.width()) {
						ele_left.animate({'width':0},300);
						ele_left.find('.slidemenu').animate({'width':0},300);
						ele_right.animate({'margin-left':right_initialWidthToLeft},300);
						splitbar_toggleicon.html('<i class="icon iconfont">&#xe65f;</i>');
					}else{
						ele_left.animate({'width':left_width},300)
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

	$.fn.extend({
		//自动补全插件
		hasngAutoComplete:function(options){
        	//默认参数
		    var defaluts = {
		    	height:null, //number 自动填充容器高度 可空（默认自动高度）
		    	width:null, //number  自动填充容器宽度 可空（默认与实例对象等宽）
		        targetInputId:null, //#id 目标文本框id（即选择后赋值文本框 非直接文本框进行的实例需赋值）
		        size:10,	//默认取前十个匹配
		        data:{	//数据相关
		        	dataSource:'',	//数据源 	必填
		        	textField:'',	//数据源单个对象文本列名	必填
		        	valueField:''	//数据源单个对象值列名	必填
		        }
		    };
            var opts = $.extend({}, defaluts, options);

            var that = $(this);
            var that_id = that.attr('id');
            var complete_width = opts.width||that.innerWidth();
            var targetInput = $(opts.targetInputId).length>0?$(opts.targetInputId).eq(0):that;
            var data_type;
            var typeofData = typeof(opts.data.dataSource);
            var dataLength = 0;//返回数据数量
            if (!opts.data.dataSource) {alert('请给data参数');return;};
            if (opts.data.dataSource&&typeofData==='string') {data_type="url";};
            if ($('#hasng_complete_'+that_id).length<1) {$('body').append('<ul id="hasng_complete_'+that_id+'" class="hasng-complete" style="z-index:1000000"></ul>');};
            var hasngcomplete = $('#hasng_complete_'+that_id).eq(0);
            


            targetInput.keyup(function(event){
            	
            	var keycode = event.which;
            	if(keycode===13||keycode===38||keycode===40){
            		/*var complete = $('#hasng_complete').eq(0);
            		complete_lis = complete.find('>li');
	            	complete_currentli = complete.find('>li.active');
            		if (keycode===13) {
            			//获取选中值
            			alert('确定选择');
	            	}else if(keycode===38){
	            		//上翻 
	            		//无选中值 选中最后一个    有选中值选中当前上一个 
	            		alert('上翻');
	            		if (complete_lis.length&&!complete_currentli) {
	            			complete_lis.eq(complete_li.length-1).addClass(active);
	            		};
	            	}else if(keycode===40){
	            		//下翻
	            		alert('下翻');
	            	}*/
            	}else{
            		//alert('其他键盘,当前输入值：'+targetInput.val());
            		if (data_type==='url') {
	            		$.ajax({
		            		url:opts.data.dataSource,
		            		method:'GET',
		            		dataType:'json',
		            		//async:false,  wjm在1.23日修改
							async:true,
		            		data:{size:opts.size,keyword:trimStr(targetInput.val())},
		            		success:function(data){
		            			var keyValue = $("#"+that_id).val();
		            			dataLength =0;
		            			var li_html = getCompleteHtml(data,keyValue);
		            			
		            			
		            			hasngcomplete.html(li_html)
		            			.css({
					            	'top':that.offset().top+that.outerHeight(),
					            	'left':that.offset().left,
					            	'width':complete_width,
					            	'height':opts.height||'auto'
					            });
					            if (hasngcomplete.html()) {hasngcomplete.show()};
		            		},
		            		error:function(){
		            			//console.log('请求后台数据出错！');
		            			hasngcomplete.hide();
		            		}
		            	});
	            	};
            	}
            	
            });
			
			
			
			$(document).click(function(){
				hasngcomplete.hide();
				$("#"+that_id).attr("data-seled","");
//				
			})
			.on('click','#hasng_complete_'+that_id+'>li',function(){
				var data_text = $(this).text();
				var data_value = $(this).attr('data-value');
				targetInput.val(data_text)
				.attr('data-value',data_value);
				$("#"+that_id).attr("data-seled","");
				$("#"+that_id).blur();//鼠标点击下拉项条目后，文本框失去焦点
			});
			
			//去除首尾空格
			function trimStr(str){
				return str.replace(/(^\s*)|(\s*$)/g,"");
			}

            //组合自动填充html结构
            function getCompleteHtml(data,keyValue){
    			var li_html = '';
    			if (data&&data.length>0) {
	    				for (var i = 0; i < data.length; i++) {
	    					if(keyValue==""){hasngcomplete.html(""); return};
	    					var index = data[i][opts.data.textField].indexOf(keyValue);
    						if(index > -1){
    							dataLength++;
    							if(dataLength>opts.size){
    								return li_html
    							}
    							var markData = data[i][opts.data.textField].replace(keyValue,"<span style='color:#000;font-weight:bold;'>"+keyValue+"</span>")
    							li_html+='<li data-value="'+data[i][opts.data.valueField]+'">'+markData+'</li>'
    						}
		    				
		    			};
	    			};
    			return li_html;
            }
           
          
            //键盘功能添加
//        
        	$(document).on("focus","#"+that_id,function(){
				var _this = this;
				var count = -1;
				$(document).keyup(function(event){
					if($("#hasng_complete_"+that_id).css("display")=="block"){
							var keyCode = event.keyCode;
							if($("#hasng_complete_"+that_id).css("display")=="none")count=count;
						
							if(keyCode==40){
								count++;
								if(count>=dataLength-1)count=dataLength-1;//显示条数
								event.preventDefault();
								$("#hasng_complete_"+that_id+">li").css("background","#fff").eq(count).css("background","#eee");
								$("#"+that_id).attr("data-seled","true");
								//设置是否选中标识，防止没选中条目情况下，按下回车键出现异常
							
							
							}else if(keyCode==38){
								event.preventDefault();
								count--;
								if(count<=0)count=0;
								$("#hasng_complete_"+that_id+">li").css("background","#fff").eq(count).css("background","#eee");
								$("#"+that_id).attr("data-seled","true")
							}
							if(keyCode==13){//回车
		
								if($("#"+that_id).attr("data-seled")){
									//对标签进行过滤
									var filterData = $("#hasng_complete_"+that_id+">li").eq(count).html().replace(/<.*?>/ig,"");
									$("#"+that_id).val(filterData);
									
								}else{
									$("#"+that_id).val($("#"+that_id).val());
										
								}
								$("#hasng_complete_"+that_id).css("display","none");
								$("#"+that_id).attr("data-seled","")
								count = -1;	
								$("#"+that_id).blur()
								
								
						}
					
					}
					
					
				}); 
			})
            
            
            //键盘功能添加结束

            return {
				getText:function(){return targetInput.val();},
				getValue:function(){return targetInput.attr('data-value');}
			};

		}
	});
})(window.jQuery);




$(function(){

	common_pageEventInit();
	common_pageLayoutInit();

});

/*公共事件绑定初始化*/
var common_pageEventInit = function(){
	common_selectEventInit();

	
	/*panelEventInit();*/
	common_tabEventInit();
	common_slideMenuEventInit();
}
/*公共布局初始化*/
var common_pageLayoutInit = function(){
	tableBodyScrollInit();
}

/*table body 初始化滑动效果*/
var tableBodyScrollInit = function(){
	var tables = $(".table");
	var table_bodys = $(".table-body:not(.no-scroll)");
	// if (tables.length>0) {
	// 	tables.mCustomScrollbar({
	// 	    theme:"minimal-dark",
	// 	    mouseWheelPixels:50,
	// 	    scrollInertia:0,
	// 	    //scrollEasing:"linear",
	// 	    axis:"x"
	// 	});
	// };
	if (table_bodys.length>0) {
		table_bodys.mCustomScrollbar({
		    theme:"minimal-dark",
		    mouseWheelPixels:50,
		    scrollInertia:0,
		    scrollEasing:"linear",
		    axis:"y"
		});
	};
	
}




/*面板-panel 事件初始化*/
/*var panelEventInit = function(){
	$('.panel').on('click','.icon-slide',function(){
		var panel = $(this).parent().parent().parent();
		var panel_body = panel.find('.panel-body').eq(0);
		panel_body.slideToggle(200,function(){
			panel.toggleClass('bottom-border-hide');
		});
	});
}*/

/*选项卡切换-tab 事件初始化*/
 var common_tabEventInit = function(){
 	$('.hx-tab').on('click','.hx-tab-nav-item',function(){
		var that = $(this);
		var currentIndex = that.index();
		var currentContent = that.parent().parent().find('.hx-tab-content .hx-tab-content-item').eq(currentIndex);
		that.addClass('active').siblings().removeClass('active');
		currentContent.addClass('active').siblings().removeClass('active');
		currentContent.show().siblings().hide();
	});
 }



/*下拉框 事件相关*/
//参数dir：判断展开方向（上/下）

var common_selectEventInit = function(dir,notDom){
	var dir = dir || "down";
	var notDom = notDom || "";
	var speed = 200;
		$('div.select').not(notDom).on('click',function(event){
			
			event.stopPropagation();
			var $this = $(this);
			if(dir=="down"){
				//console.log("down")
				$('div.select').not($this).find('ul.select-options').slideUp(speed,function(){
					$(this).parent().removeClass('zindex-high');
				});
				$this.addClass('zindex-high').find('ul.select-options').stop().slideToggle(speed);
			}else if(dir=="up"){
				if($this.find('ul.select-options').css("display")=="none"){
					var optionsHeight = $this.find('ul.select-options').outerHeight();			
					$('div.select').not($this).find('ul.select-options').slideUp(speed,function(){
						$(this).parent().removeClass('zindex-high');
					});
					$this.find('ul.select-options').css("display","block");
					$this.find('ul.select-options').stop().animate({"top":-1*(optionsHeight)+"px"},200);
				}else{
					$('ul.select-options').slideUp(speed,function(){
						//console.log("完毕")
						$(notDom).children("ul.select-options").css({"top":"100%","display":"none"})
					});
				}
				
			}else{
				return;
			}
		});
	


	$('ul.select-options').on('click','>li',function(event){
		event.stopPropagation();
		var option_text = $(this).text();
		var option_value = $(this).attr('data-value');
		$(this).parent().stop().slideUp(speed).parent().attr({'data-value':option_value,'data-text':option_text}).find('.select-text').text(option_text);
//		console.log($(notDom))
		$(notDom).children("ul.select-options").css({"top":"100%","display":"none"})
	});

	$(document).click(function(){
		$('ul.select-options').slideUp(speed,function(){
			$(notDom).children("ul.select-options").css({"top":"100%","display":"none"});
		
		});
		
		
	});

}

/*侧边菜单栏相关事件*/
var common_slideMenuEventInit = function(){
	var slidemenu = $('.slidemenu');
	if (!slidemenu||!slidemenu.length) {return;};

	$(document).on('click','.slidemenu-item',function(){
		$(this).addClass('active').siblings().removeClass('active').end().siblings('.isparent').find('.slidemenu-item-childmenu').slideUp();
	});

	$(document).on('click','.slidemenu-item.isparent',function(){
		$(this).find('.slidemenu-item-childmenu').slideToggle();
	});
}

