//左侧边栏框架
//window.onload=function(){


$(function () {


    //左侧菜单栏
    $(".menu-item-list .head-title").on("click.a", function () {

        //判断list-slide是否是block状态 切换标题右侧箭头
        var $slidebloock = $(this).parent('.menu-item-heade').find('.list-slide')
        //重置ul高度
        $slidebloock.height($(this).next('ul').children().size() * 40 + 'px')

        var $slidedisplay = $slidebloock.css('display')
        if ($slidedisplay == "none") {
            $(this).find('.icon-slide').addClass('icon-jiantou-copy-copy').removeClass('icon-jiantou-copy')
        } else {
            $(this).find('.icon-slide').addClass('icon-jiantou-copy').removeClass('icon-jiantou-copy-copy')
        }
        $(this).parent('.menu-item-heade').siblings().find('.icon-slide').removeClass('icon-jiantou-copy-copy').addClass('icon-jiantou-copy')
        //二级菜单上拉下滑
        $(this).next().stop().slideToggle(300).parent().siblings().children(".list-slide").stop().slideUp(300);
        $(".menu-item-list .head-title").removeClass("head-title-current").children("span").removeClass("head-title-span");
        $(this).addClass("head-title-current").find("span").addClass("head-title-span");
        $(this).find('span').addClass('head-title-span-current').parents('.menu-item-heade').siblings('li').find('.head-title').find('span').removeClass('head-title-span-current')
        $(this).siblings('.list-slide').find('.item-title').css('color', '#CBEBFF')
        $(this).siblings('.list-slide').children("li").removeClass("slide-item-current")
        $('.item-zhezhao').css('display', 'none')

        //左侧侧边栏选中侧边bar有颜色
        $(this).find('.line-bar').addClass('line-bar-sty')
        $(this).parents('.menu-item-heade').siblings().find('.line-bar').removeClass('line-bar-sty')
        $(this).next('.list-slide').find('.line-bar').addClass('line-bar-sty')
        $(this).parents('.menu-item-heade').siblings().next('.list-slide').find('.line-bar').removeClass('line-bar-sty')
      

    })

    $(".menu-item-list .list-slide li").on("click", function (event) {
        event.stopPropagation()
    })

    //hover第二级菜单时带三级菜单靠近下方的朝上显示
    $(".menu-item-list .list-slide .li-item").hover(function () {
        var $liindex = $(this).parents('.menu-item-heade').index()
        if ($liindex > 3) {
            $(this).css('position', 'relative')
            $(this).find('.second-ul').css({
                'position': 'absolute',
                'bottom': '0px'
            })
        }
    })
    //hover 第二个ul 图标不出现 hover时展开 收缩状态下三级菜单状态，及小箭头是否出现
    var $aimlis = $('.menu-item-heade .list-slide .li-item')
    $aimlis.live('mouseover', function () {
        $('.mCSB_scrollTools').css('z-index', '-100')
        $(this).find('.second-ul').css('display', 'block')
        $(this).parents('.menu-item-heade').find('.item-zhezhao').css('display', 'none')
        if (flag == true) {
            //判断二级菜单是否有三级菜单，如果有，就出现右侧小箭头，如果没有，就不出现小箭头
            var threeul = $(this).find('.second-ul').length
            if (threeul == false) {
                $(this).find('.ul-second-icon').css('display', 'none')
            } else {
                $(this).find('.ul-second-icon').removeClass('icon-jiantou-copy').css('display', 'inline-block')

            }
        } else {
            $(this).find('.ul-second-icon').css('display', 'none')
        }

    })
    $aimlis.live('mouseout', function () {
        $('.mCSB_scrollTools').css('z-index', '100')
        $(this).find('.ul-second-icon').css('display', 'none')
        $(this).find('.second-ul').css('display', 'none')
    })

    //右侧头部
    $(".head-right .icon-hover").on("click", function () {
       // $(this).addClass("setting-current").siblings().removeClass("setting-current")
        //$(this).find('span').addClass('span-current').parents('.icon-hover').siblings().find('.icon-right').removeClass('span-current')
        //console.log($(this).find('span'))
    })

    /*点击首页其他小列表消失*/
    var $shouyetitle = $('.shouyetitle')
    $shouyetitle.on('click', function () {
        $('.menu-item-list .list-slide').slideUp(150)
        $(".iframe-content").css("bottom", "0px");//93px
    })

    //点击三线按钮隐藏左侧边栏
    var $btn = $('.slide-icon');
    var $bigtitle = $('.head-title .big-title');
    var $iconslide = $('.head-title .icon-slide');
    var $logocontent = $('.logo .logo-content');
    var $logotext = $('.logo .logo-text')
    var $headiconfont = $('.head-title .icon-left')
    var $logo = $('.logo')
    var $logoelement = $('.logo-element')
    var $leftmenuarea = $('.left-menu-area')
    var $menue = $('.menue')
    var $rightcontentarea = $('.right-content-area')
    var $iconleft = $('.head-title .icon-left')
    var $menue = $('.menue')
    var $headleft = $('.head-left')
    var $headorign = $('.head-orign')
    var $headright = $('.head-next')
    var $mscroll = $('#mCSB_2_scrollbar_vertical')
    var $li = $('#menu-item-list .head-title')
    var menulist = $('.menu-item-list')
    var $itemli = $('.menu-item-list li')
    var $listslide = $('.menu-item-list li .list-slide')
    var $listslideli = $('.menu-item-list li .list-slide li')
    var $secondul = $('.second-ul')
    var $lispan = $listslideli.find('.item-title')
    var $headertitle = $itemli.find('.head-title')
    var $ulseconticonfont = $('.menue .list-slide .item-title .iconfont')
    var flag = false;
    $btn.toggle(function () {
        flag = true
        $(this).removeClass('icon-spread-h-bar').addClass('icon-chanpinshuomingsanjiaoxing')
        $('.head-list-title .big-title').css('display', 'none');
        $bigtitle.css('display', 'none')
        $iconslide.css('display', 'none')
        $logocontent.css('display', 'none')
        $logotext.css('display', 'none')
        $logoelement.css('display', 'inline-block')
        $leftmenuarea.animate({
            'width': '70px'
        }, 50, function () {
            setTimeout(function () {
                tabnav.init();
            }, 0)
        })
        $rightcontentarea.animate({
            'margin-left': '71px'
        }, 50)
        $iconleft.css('font-size', '24px')
        $menue.css('width', '220px')
        $headorign.css('display', 'none')
        $headright.css('display','inline-block')
        $menue.css({
            'top': '0px',
            'width': '70px'

        })
        $li.css('width', '70px')
        $mscroll.css('left', '70px')
        //      menulist.css('width', '220px')
        $headiconfont.css('margin', '0 10px 0 21px')
        $lispan.css('margin-left', '19px')
        $secondul.css('left', '100%')
        $('.menu-item-heade .head-title').css({
            'width': '70px'
           
        })
        $('.menu-item-heade .head-list-title').css('width', '70px')
        $('.menu-item-heade .head-list-title .iconfont').css({
            'font-size': '24px',
            'margin':'0 10px 0 21px'
        })
        $btn.animate({
            'left': '70px'
        }, 50)
        if (flag == true) {
         
            $(".menu-item-list").find(".list-slide").css('display', 'none')
            // 判断是左侧菜单是否为闭合状态 是的话则点击li遮罩消失
            $(".menu-item-list .list-slide li").on("click", function () {
                if (flag == true) {
                    var $zhezhao = $('.zhezhao')
                    $zhezhao.css('display', 'none')
                    $('.item-zhezhao').css('display', 'none')
                }
            })
            //			$(".menu-item-list .head-title").off('click.a')
            $(".menu-item-list .head-title").off('click.c')
            $(".menu-item-list .head-title").on("click.b", function (event) {
                event.stopPropagation();
                //左侧侧边栏收缩时点击一级菜单 二级菜单上的左侧黄色线消失
                $(this).next('.list-slide').find('.line-bar').removeClass('line-bar-sty')

                var $zhezhao = $('.zhezhao')
                if (!$(this).hasClass('shouyetitle')) {
                    if (flag == true) {
                        // 显示遮罩
                        $zhezhao.css('display', 'block')
                    }
                } else {
                    //隐藏遮罩
                    $zhezhao.css('display', 'none')
                }

                $(this).next().parent().siblings().children(".list-slide").css('display', 'none')
                var $aimlist = $(this).parents('li').find('.list-slide')
                var $aimli = $aimlist.find('.li-item')
                $itemli.css('position', 'relative')
                $listslide.css({
                    'position': 'absolute',
                    'width': '240px'
                })
                $('.left-menu-area .mCustomScrollBox').css('overflow', 'visible')
                $('.left-menu-area .mCSB_container').css('overflow', 'visible')
                $('.left-menu-area .left-menu-area').css('overflow', 'visible')

                $aimlist.css({
                    'display': 'block',
                    'position': 'absolute',
                    'z-index': '2000',
                   
                    'margin-left': '70px'
                })
                $headertitle.css('width', '70px')
                $aimli.css({
                    'padding-left': '0'
                })

                var $thiszhezhao = $(this).parent('.menu-item-heade').find('.item-zhezhao')
                $thiszhezhao.css('display', 'block')
                $thiszhezhao.on('click', function () {
                    $(this).css('display', 'none')
                    $('.zhezhao').css('display', 'none')
                })


            })



            $('.second-ul').hover(function () {
                return;
            }, function () {
            })

        }

        /*判断是否是中间偏下 中间偏下的话小列表朝上显示*/
        var $listli = $('.menu-item-list .head-title')
        $listli.click(function () {
            var $menuliindex = $(this).parent().index()
            var $listslide = $(this).parent().find('.list-slide')
            var $listheight = $listslide.innerHeight()
            /* if ($menuliindex > 5) {
                 $listslide.css('bottom', '0')
             } else {
                 $listslide.css('top', '0')
             }*/
            $listslide.css('top', '0')
        })

        /*点击document小列表消失*/
        var $leftarea = $('.left-menu-area .list-slide')
        var $zhezhao = $('.zhezhao')
        $(document).on('click', function () {
            //              $leftarea.css('display', 'none')
            $zhezhao.css('display', 'none')
            $('.item-zhezhao').css('display', 'none')
        })
        /*点击遮罩他自己消失*/
        $zhezhao.on('click', function () {
            $leftarea.css('display', 'none')
            $(this).css('display', 'none')
        })
        //当二级菜单没有三级菜单时，收缩状态时点击二级菜单，鼠标离开二级菜单消失
        var $liitem = $('.list-slide .li-item')
        $liitem.on('click', function () {
            $zhezhao.css('display', 'block')
        })
        //遮罩hover时二级菜单 三级菜单都消失
        $zhezhao.hover(function () {
            if (flag == true) {
                $('.list-slide').css('display', 'none')
            }
            $('.second-ul').css('display', 'none')
            $(this).css('display', 'none')

        })

    },
        function () {
            $('.left-menu-area .menue .head-list-title').css('display', 'block')
            $('.head-list-title .big-title').css('display', 'block');
            $btn.animate({
                'left': '220px'
            }, 50)
            $(this).removeClass('icon-chanpinshuomingsanjiaoxing').addClass('icon-spread-h-bar')
            $bigtitle.css('display', 'block')
            $iconslide.css('display', 'block')
            $logocontent.css('display', 'block')
            $logotext.css('display', 'block')
            $logoelement.css('display', 'none')
            $leftmenuarea.animate({
                'width': '220px'
            }, 50, function () {
                setTimeout(function () {
                    tabnav.init();
                }, 0)
            })
            $rightcontentarea.animate({
                'margin-left': '221px'
            }, 50)
            $iconleft.css('font-size', '17px')
            $('.menu-item-heade .head-list-title .iconfont').css({
                'font-size': '17px',
                'margin': '0 10px 0 24px'
            })
            $menue.css('width', '220px')
            $headright.css('display', 'none')
            $headorign.css('display','inline-block')
            $li.css('width', '70px')
            $headiconfont.css('margin', '0 10px 0 24px')
            $secondul.css('left', '220px')
            $('.menu-item-heade .head-title').css({
                'width': '220px',
                'height':'40px'
            })
            $listslide.css({
                'width': '220px',
                'padding-top': '0'
            })

            //      $shouyetitle.on('click.c', function() {
            //		    $('.menu-item-list .list-slide').slideUp(300,function(){
            //		    	 $listslide.css({
            //                  'width': '220px'
            //              })
            //		    })
            //		})
            flag = false
            if (flag == false) {
                Slide($(".menu-item-list .head-title"))
                $(".menu-item-list .head-title").parent().siblings().children(".list-slide").css('display', 'none')
                //         $(".menu-item-list .head-title").off('click.a')
                $(".menu-item-list .head-title").off('click.b')
                //         $(".menu-item-list .head-title").on("click.c", function() {
                //                  Slide($(this))
                //                  $('.item-zhezhao').css('display', 'none')
                //              })
                /*展开的时候遮罩消失*/
                var $zhezhao = $('.zhezhao')
                $zhezhao.css('display', 'none')

                $('.menu-item-heade .list-slide .li-item').find('.ul-second-icon').css('display', 'none')

                //当展开状态时hover离开三级菜单时二级菜单不消失
                $('.second-ul').hover(function () {
                    return;
                }, function () {
                    $(this).parents('.list-slide').css('display', 'block')
                })

                $('.item-title').css('margin-left', '35px')
            }
        })
    //展开状态时 二级菜单小箭头图标不存在
    $('.menu-item-heade .list-slide .li-item').find('.ul-second-icon').css('display', 'none')

    //展开的时候列表滑动函数
    function Slide(that) {
        //   $(that).next().stop().slideToggle(300).parent().siblings().children(".list-slide").stop().slideUp(300);
        var $headertitle = $itemli.find('.head-title')
        var $aimlist = that.parents('li').find('.list-slide')
        var $aimli = $aimlist.find('.li-item')
        $itemli.css('position', 'static')
        $listslide.css('position', 'static')
        $('.mCustomScrollBox').css('overflow', 'hidden')
        $('.mCSB_container').css('overflow', 'hidden')
        $('.left-menu-area').css('overflow', 'hidden')
        $aimlist.css({
            'position': 'static',
            'z-index': '2',
          
            'margin-left': '0',
            'width': '220px'
        })
        //$aimli.css('padding-left', '40px')
        $headertitle.css('width', '220px')
        $('.menu-item-heade .head-list-title').css('width', '220px')
        //$('.item-title').css('margin-left', '23px')
    }


    //头部右边click iconfont变色
    var $iconright = $('.content-head .head-right .icon-hover');
    var $iconbtn = $('.icon-right')
    //$iconright.click(function() {
    //      $(this).children('.icon-right').css('color', 'whiteimportant')
    //})
    /*点击切换右边iframe内容区*/

    var $indextitle = $('.indextitle')
    var $iframe = $('iframe')
    var iframe = document.getElementById("iframeId");

    $('.zhezhao-sec').hover(function () {
        $(this).css('display', 'none')
        if (flag == true) {
            $('.list-slide').css('display', 'none')
        }
    })
    $indextitle.on('click', function (event) {
        event.stopPropagation()
        $('.zhezhao-sec').css('display', 'block')
        var that = $(this)
        var $indexhref = $(this).attr('href')
        $(this).siblings().removeClass('li-item-sty')
        $(this).parents('.li-item').find('.item-title').addClass('li-item-sty')
        $(this).parents('.li-item').siblings().find('.item-title').removeClass('li-item-sty')
        $(this).parents('.menu-item-heade').siblings().find('.item-title').removeClass('li-item-sty')
        ////获取子iframe中jQuery对象
        //iframe.onload = function() {
        //    var liindex = that.index()
        //    var nowli = $(window.frames[0].document).contents().find(".leftul li").eq(liindex)
        //    nowli.addClass('active').siblings().removeClass('active')
        //    //var nowlist = $(window.frames[0].document).contents().find('iframe')
        //    //nowlist.attr('src', 'list' + liindex + '.html')
        //}


        //switch ($indexhref) {
        //    case 'index-index-more.html':
        //        $iframe.attr('src', $indexhref);
        //        break;
        //    case 'index-part-page.html':
        //        $iframe.attr('src', $indexhref);
        //        break;
        //    default:
        //        break;
        //}

    })


    $(".indextitle").click(function () {
        var o = $(this);
        document.getElementById("iframeId").src = o.attr("href");
         $('#iframeId').attr('src', o.attr("href"))
        
        //控制头部 by zxg
        var positionText = $(this).parent().prev().find(".sec-title").html();//没有去除首尾空格
        setTimeout(function () {
            $(".head-position-c").css("display", "block").children(".position-text").text(positionText);
        }, 300);
       $(".iframe-content").css("bottom", "42px");//93px
    })

    //list-slide 出现滚动条



    //左侧内容区滚动条
    $('.menue').mCustomScrollbar({
        theme: "minimal-dark",
        mouseWheelPixels: 500,
        scrollEasing: "linear",
        axis: "y",
        mouseWheelPixels: 200
    });
    //在执行完收缩展开功能时重新执行头部导航动态变化代码
    var tabnav = {
        layoutInit: function () {

            var tabnav_width = 0;
            $('.tabnav>.left>ul>li').each(function (index, el) {
                tabnav_width += $(el).outerWidth();
            });
            $('.tabnav>.left>ul').width(tabnav_width + 15);
        },
        eventBind: function () {
            $(document).on('click', '.tabnav>.left>ul>li', function () {
                var li = $(this);
                var left = $('.left').eq(0);
                var ul = $('.tabnav>.left>ul').eq(0);

                li.addClass('active').siblings().removeClass('active');

                //监测li是否被遮挡 是左边遮挡 是右边遮挡
                //遮挡：完全显示出来

                var left_offset_left = left.offset().left;
                var li_offset_left = $(this).offset().left;
                var left_offset_right = left_offset_left + left.outerWidth();
                var li_offset_right = li_offset_left + li.outerWidth() + 15;

                var ul_pos_left = parseInt(ul.css('left').replace('px', ''));
                if (li_offset_left < left_offset_left) {
                    ul.stop().animate({ left: ul_pos_left + (left_offset_left - li_offset_left) }, 200);
                }
                if ((li_offset_right - left_offset_right) > 0) {
                    ul.stop().animate({ left: ul_pos_left - (li_offset_right - left_offset_right) }, 200);
                }
            });

            $(document).on('click', '.tabnav>.right>a.pre', function () {
                var unitWidth = $('.tabnav>.left').width();
                var ul_posleft = parseInt($('.tabnav>.left>ul').css('left').replace('px', ''));
                var ul_left = $('.tabnav>.left>ul').css('left').replace('px', '')
                var result = ul_posleft += unitWidth;
                result = result > 0 ? 0 : result;
                if ($('.left').outerWidth() > $('.left>ul').outerWidth() && ul_left == 0) { $('.left>ul').css('left', '0!important'); return }
                $('.tabnav>.left>ul').stop().animate({ left: result }, 200);
            });

            $(document).on('click', '.tabnav>.right>a.next', function () {
                var unitWidth = $('.tabnav>.left').width();
                var ul = $('.tabnav>.left>ul');
                var ul_right = ul.css('right').replace('px', '')
                var ul_width_poor = ul.width() - $('.left').width()
                var ul_posleft = parseInt(ul.css('left').replace('px', ''));
                var ul_left = $('.tabnav>.left>ul').css('left').replace('px', '')
                var ul_width = ul.outerWidth();
                var result = ul_posleft - unitWidth;
                var left_width = $('.tabnav>.left').outerWidth();
                result = (Math.abs(ul_posleft) + left_width) > ul_width / 2 ? unitWidth - ul_width : result;
                if ($('.left').outerWidth() > $('.left>ul').outerWidth() && ul_left == 0) { $('.left>ul').css('left', '0!important'); return }
                if (ul_left < 0 && ul_width_poor <= 0) {
                    result = 0
                }
                $('.tabnav>.left>ul').stop().animate({ left: result }, 200);
            });
        },
        init: function () {
            this.layoutInit();
            this.eventBind();
        }
    };

    tabnav.init();
    if (window.addEventListener) {
        window.addEventListener("resize", function () {
            tabnav.init();
        });
    }
    else if (window.attachEvent) {
        window.attachEvent("resize", function () {
            tabnav.init();
        });
    }

    //点击二级菜单 直接跳转到导航页面
    var $itembtns = $('.list-slide .item-one')
    $itembtns.on('click', function () {
        //var second_display = $('.second-ul').css('display')

        //iframe.onload = function () {
        //    var nowli = $(window.frames[0].document).contents().find(".leftul li").eq(0)
        //    nowli.addClass('active').siblings().removeClass('active')
        //    var nowlist = $(window.frames[0].document).contents().find('iframe')
        //    //nowlist.attr('src', 'list0.html')
        //}
        //获取所点击菜单的href 赋值给iframe 直接跳转
        var $onehref = $(this).attr('href')
        $iframe.attr('src', $onehref);

        //点击没有三级菜单的二级菜单项目时候  字体颜色变白 其他项目样式取消
        $(this).parents('.li-item').find('.item-title').addClass('li-item-sty')
        $(this).parents('.li-item').siblings().find('.item-title').removeClass('li-item-sty')
        $(this).parents('.menu-item-heade').siblings().find('.item-title').removeClass('li-item-sty')
        
         //控制头部位置 by zxg
        var positionText = $(this).find(".sec-title").html();//没有去除首尾空格
        setTimeout(function () {
            $(".head-position-c").css("display", "block").children(".position-text").text(positionText);
        }, 300);
        $(".iframe-content").css("bottom", "42px");

    });

    //}

    //点击个人设置出现下拉框
    $('.tool-btn').click(function () {
        $(this).parent().children('.myself-icon').stop().slideToggle('fast');
    });
    $('.myself-icon li').click(function () {
        $(this).addClass('li-current').siblings().removeClass('li-current')
    });
    $('.myself-con').mouseleave(function () {
        //console.log('lll')
        $(this).children('.myself-icon').hide()
        $(this).find('li').removeClass('li-current')
    });
})
