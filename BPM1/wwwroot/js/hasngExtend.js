var hasng = {
    DelConfirm: function (fun) {
        mini.confirm("确定删除选中记录？", "提示",
            function (action) {
                $('.mini-button').removeClass('icon-style');
                if (action == "ok") {
                    fun();
                }
            });
        
    },
    Alert: function () {
        mini.alert("请选择一条数据！", "信息", function () { $('.mini-button').removeClass('icon-style'); });
    },
    WinOpen: function (objOpen) {
        mini.open({
            url: objOpen.url,
            title: objOpen.title,
            width: objOpen.width,
            height: objOpen.height,
            allowResize: false,
            showModal: true,
            showMaxButton: true,
            headerStyle: "font-weight:bold;color:#666;background:white;line-height:40px;padding:7px 0;",
            onload: function () {
                var iframe = this.getIFrameEl();

                objOpen.onload(iframe);
            },
            ondestroy: function (action) {
                var iframe = this.getIFrameEl();
                $('.mini-button').removeClass('icon-style');

                objOpen.ondestroy(action,iframe);
            }
        });
    },
    WinOpenMax: function (objOpen) {
        mini.open({
            url: objOpen.url,
            title: objOpen.title,
            width: objOpen.width,
            height: objOpen.height,
            allowResize: false,
            showModal: true,
            showMaxButton: true,
            headerStyle: "font-weight:bold;color:#666;background:white;line-height:40px;padding:7px 0;",
            onload: function () {
                var iframe = this.getIFrameEl();

                objOpen.onload(iframe);
            },
            ondestroy: function (action) {
                var iframe = this.getIFrameEl();
                $('.mini-button').removeClass('icon-style');

                objOpen.ondestroy(action, iframe);
            }
        }).max();
    }
};