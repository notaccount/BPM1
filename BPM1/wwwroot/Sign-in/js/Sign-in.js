




    $(function(){
        /*���ٵ�¼��ʼ*/

        $(".Sign-in-content-bottom-right .sr-input .Keep-right .k-s").click(function(){
            $('.Sign-in-content-bottom').hide();
            $('.sign-in-logo').hide();
            $('.fast-ks').show();
            $('.fast-wz').show();
            $('.Return').show();
            $('.fast-login').show();
        });

        $(".Sign-in-content-top .Return").click(function(){
            $('.Sign-in-content-bottom').show();
            $('.sign-in-logo').show();
            $('.fast-ks').hide();
            $('.fast-wz').hide();
            $('.Return').hide();
            $('.fast-login').hide();
        });

        //��ά�뿪ʼ
        //$(".Sign-in-content-bottom-right .b-title .QR").click(function(){
        //    $('.QR-bj').fadeIn();
        //});

        $(".QR-bj .delete").click(function(){
           $(this).parent().fadeOut();
        });
        //��ά�����

        //��ס���뿪ʼ
        $(".Sign-in-content-bottom-right .sr-input .Keep-left .Keep-left-left").toggle(
            function(){
                $(this).css("background-image","url(images/square_check.png)");},
            function(){
                $(this).css("background-image","url()");}
        );
        //��ס�������

        /*���ٵ�½����*/
    });

