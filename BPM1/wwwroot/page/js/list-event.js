/*list0列表按钮点击变色 非miniui*/
//var $headerright=$('.list-header-right')
//$headerright.on('click',function () {
//    $(this).addClass('list-header-right-sty').siblings().removeClass('list-header-right-sty')
//})
//点击表格按钮变色 非miniui
var $btns=$('.mini-toolbar .mini-button')
$btns.click(function () {
  	$(this).addClass('icon-style').siblings().removeClass('icon-style')
})

var $btn = $('.searchBox .mini-button')
$btn.click(function () {
    $(this).addClass('icon-style').siblings().removeClass('icon-style')
})

var $btnb = $('.buttom-box .mini-button')
$btnb.click(function () {
    $(this).addClass('icon-style').siblings().removeClass('icon-style')
})

$('.search-button-my').mouseleave(function () {
    $(this).removeClass('icon-style')
})
/*新增列表 miniui*/
function add(width, height) {
	mini.open({
	    url:"pop-up-list2.html",
	    width: width, 
	    height: height,
	    allowResize:false,
	    headerStyle: "font-weight:bold;color:#666;background:white;line-height:40px;padding:7px 0;",
        title: "新增",
	    onload: function () {
	        var iframe = this.getIFrameEl();
	        var data = { action: "new"};
	    },
	    ondestroy: function (action) {
            grid.reload();
	        //窗体销毁时表格按钮去掉自定义添加的样式
	        $('.mini-button').removeClass('icon-style')
	    }
	});
}
/*查看档案 miniui*/
function check(width,height) {
            var btnEdit = this;
            mini.open({
                url:"list6.html",
                headerStyle: "font-weight:bold;color:red;background:white;line-height:40px;padding:7px 0;",
                title: "请先选择档案",
                width: width,
                height:height,
                allowResize:false,
                showMaxButton: true,
                onload: function () {
                    var iframe = this.getIFrameEl();
                },
                ondestroy: function (action) {
                    //if (action == "close") return false;
                    //alert(1);
                    if (action == "ok") {
                        var iframe = this.getIFrameEl();
                        var data = iframe.contentWindow.GetData();
                        data = mini.clone(data);    //必须
                        if (data) {
                            btnEdit.setValue(data.id);
                            btnEdit.setText(data.name);
                        }
                    }
					grid.reload();
					//窗体销毁时表格按钮去掉自定义添加的样式
	       			$('.mini-button').removeClass('icon-style')
                }
            });

        }

//使弹出框列表每一行第二个文字说明距离左边输入框远一点
$('.table-sty tr td')
$('.table-sty tr').each(function(index,item){
	$(item).find('td').eq(2).addClass('item-left')
})


$('.mini-toolbar').find('.mini-button').css('float','left')
$('.mini-toolbar').addClass('clearfix')
$('#form1').find('.mini-button').css('float', 'left')
$('#form1').addClass('clearfix')
$('.Cadrebottom').find('.mini-button').css('float', 'left')
$('.Cadrebottom').addClass('clearfix')





//ie 中 textarea不换行  ie中表格中 文字不对齐 
var navigatorName = "Microsoft Internet Explorer";
if (!!window.ActiveXObject || "ActiveXObject" in window) {
    $('td').css({
       ' word-break': 'normal',
        'white-space':' normal'
    })
} else {
    //chrome
    $('.texts').html('审意<br />批&nbsp;<br />机&nbsp;<br />关见')
    //chrome
   
    var str = $('.texts-1').html()
    if (str == null) {
       
    } else {
        str = str.replace(/\s+/g, "");
        $('.texts-1').html(str)
    }
}



//当input为readonly时，使input在ie状态下光标不出现，即不能点击input框
$('.no-focus .mini-textbox-input').attr('unselectable', 'on')

$('.no-focus .mini-buttonedit-input').attr('unselectable', 'on')