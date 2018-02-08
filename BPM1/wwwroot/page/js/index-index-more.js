//主页图部分
//让工作条目下边的线条滑动
$('.work-body-content .content-top-box:odd .common-gap').addClass('odd-sty')
$('.work-body-content .content-top-box:even .common-gap').addClass('even-sty')
$('.iframe-content').css('display','block')

	$(window).on('resize',function(){
		var $containerwidth=$('.dot-container').width()*0.1666666
		BarAnimate($(".isCurrent"));	
	})
	function BarAnimate(that){
        var $index = that.parent().index()
		var $barline=$('.bar-box .bar-line')
		var $circleitem=$('.dot-container .dot-item .dot-dot')
		var $containerwidth=$('.dot-container').width()*0.166666667
        var $padwidth = $('.dot-container').width() * 0.166666667 * 0.5 - 7.5;
		$wid=$containerwidth*$index
		var $linewidth=$barline.width()
		var $dotitemwidth=$('.dot-container .dot-item').width()
		var $dotfirst=$('.dot-color-first')
		that.find('.item').addClass('current').closest('a').siblings().find('.item').removeClass('current')		
        var $wid = $containerwidth * $index
        //alert($containerwidth)
		var $dotlinwidth=$('.dot-line').width()
		var $barbox=$('.bar-box')
		$barbox.css({
			'padding-left':$padwidth+'px'
		})
		//点击线的长度变化
		$barline.stop().animate({
			'width':$wid+'px'
		},100)		
	}
    //$('.item-container').hover(function () {
    //    BarAnimate($(this));
    //    $('.item-container').removeClass("isCurrent");
    //    $(this).addClass("isCurrent");
    //    var $cir = $('.bar-box .bar-cir')
    //    $cir.css('display', 'inline-block')
    //    /*点击灰色小圆圈消失*/
    //    var $index = $(this).parent().index()
    //    var $dots = $('.dot-container .dot-dot')
    //    var $dotaim = $dots.eq($index)
    //    $dotaim.siblings().parent().siblings().find('.dot-dot').css('display', 'inline-block')
    //    //setTimeout(function () {
    //       // $dotaim.css('display', 'none')
    //   // }, 100)

    //}, function () {
    //    $('.dot-container .dot-dot').css("display", "inline-block")
    //    $('.item-container').removeClass("isCurrent");
    //    $(this).find('.item').removeClass('current').closest('a').siblings().find('.item').removeClass('current');
       
    //    //点击线的长度变化
    //    $('.bar-box .bar-line').stop().animate({
    //        'width': 0
    //    }, 100, function () {
    //        $(".bar-box .bar-cir").fadeOut(100)
    //    })		
    //})
		
	

    //主要内容区滚动条
   
        $('.main-content').mCustomScrollbar(
            {
               theme: "minimal-dark",
                mouseWheelPixels: 500,
               scrollEasing: "linear",
                axis: "y"
                //		          mouseWheelPixels:1000，鼠标滚动中滚动的像素数目 值为以像素为单位的数值
                //		          scrollButtons:50
                //				  scrollInertia:0,使用0无滚动惯性
            }
        );
		 
	//档案统计图表
	if($('#maina').length >= 1){
		var myCharta = echarts.init(document.getElementById("maina"));
		option = {
			    tooltip: {
			        trigger: 'item',
			        formatter: "{a} <br/>{b}: {c} ({d}%)"
			    },
			    grid: {
					left: '15',
					right: '15',
					bottom: '0',
					top:'10',
					containLabel: true
				},
			    color:['#D6D6D6','#3094C4'],
			    legend: {
			    	show:false,
			        orient: 'vertical',
			        x: 'left',
			        data:['其他','总计']
			    },
			    series: [
			        {
			            name:'档案统计',
			            type:'pie',
			            radius: ['50%', '70%'],
			            avoidLabelOverlap: false,
			            hoverAnimation:false,
			            stillShowZeroSum:false,
			            startAngle:100,
			            itemStyle:{
			            	emphasis:{
			            		color:'#EA7C10'
			            	}
			            },
			            label: {
			                normal: {
			                    show: false,
			                    position: 'center'
			                },
			                emphasis: {
			                    show: true,
			                    textStyle: {
			                        fontWeight: 'bold'
			                    }
			                }
			            },
			            labelLine: {
			                normal: {
			                    show: false
			                }
			            },
			            data:[
			            	{value:800, name:'其他'},
			                {value:600, name:'总计'}
			            ],
			            markArea:{
			            	silent:true
			            }
			        }
			    ]
			};
		myCharta.setOption(option);	
	}	
	
	if($('#mainb').length >= 1){
	var myChartb = echarts.init(document.getElementById("mainb"));
		option = {
			    tooltip: {
			        trigger: 'item',
			        formatter: "{a} <br/>{b}: {c} ({d}%)"
			    },
			    grid: {
					left: '15',
					right: '15',
					bottom: '0',
					top:'10',
					containLabel: true
				},
			    color:['#D6D6D6','#56aed8'],
			    legend: {
			    	show:false,
			        orient: 'vertical',
			        x: 'left',
			        data:['其他','总计']
			    },
			    series: [
			        {
			            name:'档案统计',
			            type:'pie',
			            radius: ['50%', '70%'],
			            avoidLabelOverlap: false,
			            hoverAnimation:false,
			            stillShowZeroSum:false,
			            startAngle:200,
			            itemStyle:{
			            	emphasis:{
			            		color:'#EA7C10'
			            	}
			            },
			            label: {
			                normal: {
			                    show: false,
			                     position: 'center'
			                },
			                emphasis: {
			                    show: true,
			                    textStyle: {
			                        fontWeight: 'bold'
			                    }
			                }
			            },
			            labelLine: {
			                normal: {
			                    show: false
			                }
			            },
			            data:[
			            	{value:100, name:'其他'},
			                {value:500, name:'总计'}
			            ],
			            markArea:{
			            	silent:true
			            }
			        }
			    ]
			};
		myChartb.setOption(option);	
	}
	
	if($('#mainc').length >= 1){
		var myChartc = echarts.init(document.getElementById("mainc"));
		option = {
			    tooltip: {
			        trigger: 'item',
			        formatter: "{a} <br/>{b}: {c} ({d}%)"
			    },
			    grid: {
					left: '15',
					right: '15',
					bottom: '0',
					top:'10',
					containLabel: true
				},
			    color:['#D6D6D6','#75c8ef'],
			    legend: {
			    	show:false,
			        orient: 'vertical',
			        x: 'left',
			        data:['其他','总计']
			    },
			    series: [
			        {
			            name:'档案统计',
			            type:'pie',
			            radius: ['50%', '70%'],
			            avoidLabelOverlap: false,
			            hoverAnimation:false,
			            stillShowZeroSum:false,
			            startAngle:120,
			            itemStyle:{
			            	emphasis:{
			            		color:'#EA7C10'
			            	}
			            },
			            label: {
			                normal: {
			                    show: false,
			                     position: 'center'
			                },
			                emphasis: {
			                    show: true,
			                    textStyle: {
			                        fontWeight: 'bold'
			                    }
			                }
			            },
			            labelLine: {
			                normal: {
			                    show: false
			                }
			            },
			            data:[
			            	{value:600, name:'其他'},
			                {value:300, name:'总计'}
			            ],
			            markArea:{
			            	silent:true
			            }
			        }
			    ]
			};
		myChartc.setOption(option);	//eacharts绘制表格重新渲染	
	}		
			
	
	//人事统计图表
	if($('#maind').length >= 1){
		var myChartd = echarts.init(document.getElementById("maind"));
		option = {
			tooltip : {
				trigger: 'axis',
				axisPointer : {            // 坐标轴指示器，坐标轴触发有效
					type : 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
				}
			},
			grid: {
				left: '15',
				right: '18',
				bottom: '0',
				top:'10',
				containLabel: true
			},
			xAxis : [
				{
					type : 'category',
					data : ['正处级干部','副处级干部','超编人数','缺编人数'],
					axisLine:{
						lineStyle:{
							color:'#efefef'
						}
					},
					splitLine:{
						show:true,
						lineStyle:{
							color:['#efefef']
						}
					},
					axisLabel: {
						show: true,
						textStyle: {
							color: '#666666'
						}
					},
					nameTextStyle:{
						fontFamily:'Microsoft YaHei'
					}
					
				}
			],
			yAxis : [
				{
					type : 'value',
					min:0,
					max:400,
					axisLine:{
						lineStyle:{
							color:'#efefef'
						}
					},
					splitLine:{
						show:true,
						lineStyle:{
							color:['#efefef']
						}
					},
					axisTick:{show:false},
					axisLabel: {
						show: true,
						textStyle: {
							color: '#666666'
						}
					}
				}
			],
			series : [
				{
					name:'人事统计',
					type:'bar',
					data:[320, 332, 301, 334],
					itemStyle:{
						normal:{
							color: function (params){
								var colorList = ['#3094c4','#f3ad00','#ea7c10','#75c8ef'];
								return colorList[params.dataIndex];
							}
						}
					},
					barWidth: 24

				}

			]
		};
		myChartd.setOption(option);
	}	
	//数字化进度图表
	if($('#maine').length >= 1){
		var myCharte = echarts.init(document.getElementById("maine"));
		option = {
			tooltip: {
				trigger: 'axis',
				axisPointer: {
					type: 'shadow'
				}
			},
			grid: {
				left: '15',
				right: '18',
				bottom: '0',
				top:'10',
				containLabel: true
			},
			xAxis: {
				type: 'value',
				boundaryGap: [0, 0.01],
				min:0,
				max:180,
				splitLine:{show:false},
				interval:20,
				axisTick:{show:false},
				axisLine:{
					lineStyle:{
						color:'#efefef'
					}
				},
				axisLabel: {
					show: true,
					textStyle: {
						color: '#b5b5b5'
					}
				},
				nameTextStyle:{
					fontFamily:'Microsoft YaHei'
				}
			},
			yAxis: {
				type: 'category',
				data: ['扫描','录入','整理'],
				axisTick:{show:false},
				axisLine:{
					lineStyle:{
						color:'#efefef'
					}
				},
				axisLabel: {
					show: true,
					textStyle: {
						color: '#666666'
					}
				}

			},
			series: [
				{
					name: '数字化进度',
					type: 'bar',
					data: [100,100,50],
					itemStyle:{
						normal:{
							color: function (params){
								var colorList = ['#3094c4','#f3ad00','#ea7c10'];
								return colorList[params.dataIndex];
							}
						}
					},
					silent: true,
					barWidth: 5,
					barGap: '-30%', // Make series be overlap
					barCategoryGap:'10%'

				}
			]
		};
		myCharte.setOption(option);
	}
	//eacharts绘制表格重新渲染
	if (window.addEventListener) {
		window.addEventListener("resize",function(){
			if($('#maina').length >= 1){
				myCharta.resize();
			}
			if($('#mainb').length >= 1){
				myChartb.resize();
			}
			if($('#mainc').length >= 1){
				myChartc.resize();
			}
			if($('#maind').length >= 1){
				myChartd.resize();
			}
			if($('#maine').length >= 1){
				myCharte.resize();
			}
		});
	}
	else if(window.attachEvent){
		window.attachEvent("resize",function(){
			if($('#maina').length >= 1){
				myCharta.resize();
			}
			if($('#mainb').length >= 1){
				myChartb.resize();
			}
			if($('#mainc').length >= 1){
				myChartc.resize();
			}
			if($('#maind').length >= 1){
				myChartd.resize();
			}
			if($('#maine').length >= 1){
				myCharte.resize();
			}
		});
	}


