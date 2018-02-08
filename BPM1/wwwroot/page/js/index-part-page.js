//表格部分
//列表按钮点击变色
var $headerright=$('.list-header-right')
$headerright.on('click',function () {
    $(this).addClass('list-header-right-sty').siblings().removeClass('list-header-right-sty')
})
//查询，导出按钮hover变色
var $listcheck=$('.list-header .list-header-right')
$listcheck.hover(function(){
	$(this).css({
		'background':'#ea7c10',
		'color':'white'
	})
},function(){
	$(this).css({
		'background':'#EFEFEF',
		'color':'#666666'
	})
})
//点击下拉箭头上拉和收缩
var $toggleBtn=$('.icon-box .slide-toggle-icon')		
	$toggleBtn.toggle(function(){
		var $leftarea=$('.left-menu-area .list-slide')
		$(this).removeClass('icon-xiajiantou').addClass('icon-jiantoushang')
		$(this).parents('.father').find('.slide-wraper').slideUp('fast')
		$leftarea.css('display','none')
	},function(){
		var $leftarea=$('.left-menu-area .list-slide')
		$(this).parents('.father').find('.slide-wraper').slideDown('fast')
		$(this).removeClass('icon-jiantoushang').addClass('icon-xiajiantou')
		$leftarea.css('display','none')
	})
/*动态导航*/
//页面开始刷新时一页按钮是灰色
//var ul_left=parseInt($('.tabnav>.left>ul').css('left').replace('px',''))
//if(ul_left==0){
//	$('.tabnav>.right>a.pre').addClass('asty')
//}
var tabnav = {
			layoutInit:function(){

				var tabnav_width = 0;
				$('.tabnav>.left>ul>li').each(function(index, el) {
					tabnav_width+=$(el).outerWidth(true);
				});
				$('.tabnav>.left>ul').width(tabnav_width+15);
			},
			eventBind:function(){
				$(document).on('click','.tabnav>.left>ul>li',function(){
					var li = $(this);
					var left = $('.left').eq(0);
					var ul = $('.tabnav>.left>ul').eq(0);

					li.addClass('active').siblings().removeClass('active');
					$(this).find('.arrow-bar').addClass('arrow-bar-sty').parent().siblings().find('.arrow-bar').removeClass('arrow-bar-sty');
					
			 		
					//监测li是否被遮挡 是左边遮挡 是右边遮挡
					//遮挡：完全显示出来
					
					var left_offset_left = left.offset().left;
					var li_offset_left = $(this).offset().left;
					var left_offset_right = left_offset_left+left.outerWidth();
					var li_offset_right = li_offset_left+li.outerWidth()+15;

					var ul_pos_left = parseInt(ul.css('left').replace('px',''));
					//alert('left_offset_left:'+left_offset_left+'***li_offset_left:'+li_offset_left);
					if (li_offset_left<left_offset_left) {
						//alert('左挡');
						//alert('left_offset_left:'+left_offset_left+'***li_offset_left:'+li_offset_left);
						//alert(left_cha);
						ul.stop().animate({left:ul_pos_left+(left_offset_left-li_offset_left)},200);
					}
					if((li_offset_right-left_offset_right)>0){
						//alert('left_offset_right:'+left_offset_right+'***li_offset_right:'+li_offset_right);
						//alert('右挡');
						ul.stop().animate({left:ul_pos_left-(li_offset_right-left_offset_right)},200);
					}
					

					//alert('left_offset_right:'+left_offset_right+'***li_offset_right:'+li_offset_right);
				});

				$(document).on('click','.tabnav>.right>a.pre',function(){
					var unitWidth = $('.tabnav>.left').width();
					var ul_posleft = parseInt($('.tabnav>.left>ul').css('left').replace('px',''));
					var ul_left=$('.tabnav>.left>ul').css('left').replace('px','')
					var result = ul_posleft += unitWidth;
					result = result>0?0:result;
					if($('.left').outerWidth()>$('.left>ul').outerWidth() && ul_left == 0){$('.left>ul').css('left','0!important');return}
//					if($('.left').outerWidth()>$('.left>ul').outerWidth() && $('.left>ul').left>=0){$('.left>ul').css('left','0!important')}
					$('.tabnav>.left>ul').stop().animate({left: result},200);
					
					//列表项目往前没有时点击上一页按钮不能用
//					$('.tabnav>.right>a.next').removeClass('asty')
//					if(ul_left==0){
//						$(this).addClass('asty')
//					}
				});

				$(document).on('click','.tabnav>.right>a.next',function(){
					var unitWidth = $('.tabnav>.left').width();
					var ul = $('.tabnav>.left>ul');
					var ul_right=ul.css('right').replace('px','')
					var ul_width_poor=ul.width()-$('.left').width()
					var ul_posleft = parseInt(ul.css('left').replace('px',''));
					var ul_left=$('.tabnav>.left>ul').css('left').replace('px','')
					var ul_width = ul.outerWidth();
					var result = ul_posleft - unitWidth;
					var left_width = $('.tabnav>.left').outerWidth();
					result = (Math.abs(ul_posleft)+left_width)>ul_width/2?unitWidth-ul_width:result;
					if($('.left').outerWidth()>$('.left>ul').outerWidth() && ul_left == 0){$('.left>ul').css('left','0!important');return}
//					if($('.left').outerWidth()>$('.left>ul').outerWidth()){$('.left>ul').css('left','0!important')}
					if(ul_left < 0 && ul_width_poor <= 0){
						result = 0
					}
					$('.tabnav>.left>ul').stop().animate({left: result},200);
					//列表项目往前没有时点击下一页按钮不能用
//					$('.tabnav>.right>a.pre').removeClass('asty')
//					if(ul_right==ul_width_poor){
//						$(this).addClass('asty')
//					}
					
				});
			},
			init:function(){
				this.layoutInit();
				this.eventBind();
			}
		};

		$(function(){
			tabnav.init();
		});
//重新渲染时加载动态导航		
if (window.addEventListener) {
		window.addEventListener("resize",function(){
			tabnav.init();
		});
	}
	else if(window.attachEvent){
		window.attachEvent("resize",function(){
			tabnav.init();
		});
	}
	
