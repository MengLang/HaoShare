(function ($) {
    var _func, _n, _stor, _vars;
    _n = this;
    _vars = _n.vars;
    _func = _n.func;
    _stor = _n.storage;
    _vars.exponent = "";
    _vars.modules = "";
    _vars.timestamp = new Date().getTime();
    _vars.rsakey = "";
    _vars.pageMobileValidate = false;/*注册状态*/
    _func.isCellPhone = function (e) {
        var t = /(^0{0,1}1[3|4|5|6|7|8][0-9]{9}$)/;
        return t.test(e);
    };
    _func.isIdCardNo = function (code) {
        var city = { 11: "北京", 12: "天津", 13: "河北", 14: "山西", 15: "内蒙古", 21: "辽宁", 22: "吉林", 23: "黑龙江 ", 31: "上海", 32: "江苏", 33: "浙江", 34: "安徽", 35: "福建", 36: "江西", 37: "山东", 41: "河南", 42: "湖北 ", 43: "湖南", 44: "广东", 45: "广西", 46: "海南", 50: "重庆", 51: "四川", 52: "贵州", 53: "云南", 54: "西藏 ", 61: "陕西", 62: "甘肃", 63: "青海", 64: "宁夏", 65: "新疆", 71: "台湾", 81: "香港", 82: "澳门", 91: "国外 " };
        var tip = "";
        var pass = true;
        if (!code || !/^\d{6}(18|19|20)?\d{2}(0[1-9]|1[12])(0[1-9]|[12]\d|3[01])\d{3}(\d|X)$/i.test(code)) {
            tip = "身份证号格式错误";
            pass = false;
        } else if (!city[code.substr(0, 2)]) {
            tip = "地址编码错误";
            pass = false;
        } else {
            if (code.length == 18) {
                code = code.split('');
                var factor = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2];
                var parity = [1, 0, 'X', 9, 8, 7, 6, 5, 4, 3, 2];
                var sum = 0;
                var ai = 0;
                var wi = 0;
                for (var i = 0; i < 17; i++) {
                    ai = code[i];
                    wi = factor[i];
                    sum += ai * wi;
                }
                var last = parity[sum % 11];
                if (parity[sum % 11] != code[17]) {
                    tip = "校验位错误";
                    pass = false;
                }
            }
        }
        if (!pass) _func.showMessage(tip);
        return pass;
    };
    _func.checkDate = function (INDate) {
        if (INDate == "") {
            return true;
        }
        subYY = INDate.substr(0, 4)
        if (isNaN(subYY) || subYY <= 0) {
            return true;
        }
        //转换月份
        if (INDate.indexOf('-', 0) != -1) {
            separate = "-";
        } else {
            if (INDate.indexOf('/', 0) != -1) {
                separate = "/";
            } else {
                return true;
            }
        }
        area = INDate.indexOf(separate, 0);
        subMM = INDate.substr(area + 1, INDate.indexOf(separate, area + 1) - (area + 1));
        if (isNaN(subMM) || subMM <= 0) {
            return true;
        }
        if (subMM.length < 2) {
            subMM = "0" + subMM;
        }
        //转换日
        area = INDate.lastIndexOf(separate);
        subDD = INDate.substr(area + 1, INDate.length - area - 1);
        if (isNaN(subDD) || subDD <= 0) {
            return true;
        }
        if (eval(subDD) < 10) {
            subDD = "0" + eval(subDD);
        }
        NewDate = subYY + "-" + subMM + "-" + subDD;
        if (NewDate.length != 10) {
            return true;
        }
        if (NewDate.substr(4, 1) != "-") {
            return true;
        }
        if (NewDate.substr(7, 1) != "-") {
            return true;
        }
        var MM = NewDate.substr(5, 2);
        var DD = NewDate.substr(8, 2);
        if ((subYY % 4 == 0 && subYY % 100 != 0) || subYY % 400 == 0) { //判断是否为闰年
            if (parseInt(MM) == 2) {
                if (DD > 29) {
                    return true;
                }
            }
        } else {
            if (parseInt(MM) == 2) {
                if (DD > 28) {
                    return true;
                }
            }
        }
        var mm = new Array(1, 3, 5, 7, 8, 10, 12); //判断每月中的最大天数
        for (i = 0; i < mm.length; i++) {
            if (parseInt(MM) == mm[i]) {
                if (parseInt(DD) > 31) {
                    return true;
                }
            } else {
                if (parseInt(DD) > 30) {
                    return true;
                }
            }
        }
        if (parseInt(MM) > 12) {
            return true;
        }
        return false;
    };
    _func.isPassword = function (e) {
        //        var reg = /(?!^[0-9]+$|^[a-zA-Z]+$|^[~`!@#$%^&*()_+-=|]+$)^[0-9a-zA-Z~`!@#$%^&*()_+-=|]{6,20}/;
        //        return reg.test(e);
        if (e && e.length <= 20 && e.length >= 6) {
            return true;
        }
        return false;
    };
    _func.CheCkEmpty = function (e, name, showMessage) {
        if (!e || e.val().length <= 0) {
            showMessage(name + "不能为空!");
            e.focus();
            return false;
        }
        return true;
    };


  
    _func.checkIsName = function (e) {
        var reg = new RegExp("[0-9`~!@#$^&*()=|{}':;',\\[\\].<>/?~！@#￥……&*（）——|{}【】‘；：”“'。，、？]");
        if (reg.test(e)) {
            return false;
        }
        return true;
    };
   
    _func.getSms = function (d, n) {
        _vars.SendSms.init(n, d);
    };
    _func.loginOut = function () {
        $.ajax({
            type: "post",
            url: _vars.url.loginOut,
            data: {},
            success: function (result) {
                if (result.isSuccess == 1) {
                    N.visit(_vars.url.default);
                } else {
                    _func.showMessage(result.oMessage);
                }
            },
            error: function () {
                _func.showMessage(_vars.message.loginOutError);
            }
        });
    };
    /*短信接口*/
    _vars.SendSms = {
        node: null,
        count: 60,
        text: null,
        start: function (msg) {
            var _msg = msg;
            if (this.count > 0) {
                this.node.text(this.count-- + "秒后再次发送");
                var _this = this;
                setTimeout(function () {
                    _this.start(_msg);
                }, 1000);
                _self = this;
                _func.addBeforeUnloadCall('cleanSendSmsCount', function () {
                    _self.count = _self.count != 60 ? 0 : _self.count;
                });
            } else {
                this.clear(_msg);
            }
        },
        clear: function (msg) {
            this.node.removeAttr("disabled");
            this.node.removeClass('getPaGray');
            this.node.html(msg && msg.clear ? msg.clear : "获取验证码");
            this.count = 60;
        },
        //初始化
        init: function (n,url, d, init_f, msg) {
            this.node = $(n);
            this.text = this.node.html();
            this.node.addClass('getPaGray');
            this.node.attr("disabled", true);
            this.node.html("正在发送验证码");
            var _this = this,
                _f = init_f,
                _msg = msg;
            $.ajax({
                type: "POST",
                url: url,
                dataType: 'json',
                async: false,
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                data: d,
                success: function (data) {
                    _vars.token = data.FormToken;
                    if (data.Result) {
                        _func.showMessage(data.Message);
                            _this.start(_msg);
                            if (_f) {
                                _f();
                            }
                    } else {
                        _func.showMessage(data.Message);
                        _this.clear(_msg);
                    }
                },
                error: function (data) {
                    _func.showMessage("服务器调用失败");
                    _this.clear();
                }
            });
        }
    };

    _func.getQueryStringByName = function (name) {
        var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));
        if (result == null || result.length < 1) {
            return "";
        }
        return result[1];
    };
    _func.showMessage = function (text) {
        _vars.errorDiv.html(text);
        window.scrollTo(0, 0);
        _vars.errorDiv.removeClass('Ctip_animation').addClass('Ctip_animation');
        _vars.errorDiv.css('display', 'block');
    };
  

    _func.clearMessage = function () {
        _vars.errorDiv.html('');
        _vars.errorDiv.css('display', 'none');
    };
    _vars.message = {
        register: {
            error: "注册出现错误!",
            errorName: "姓名不正确"
        },
        login: {
            error: "登录出现错误!",
            loginOutError: "退出登录出现错误"
        },
        resetPassword: {
            complete: "重置密码成功!",
            error: "重置密码出现错误"
        },
        moreInfo: {
            error: "完善信息出现错误!"
        },
        sms: {
            error: "验证码发送失败",
            coomplete: "验证码发送成功"
        },
        order: {
            cancelError: "取消订单出现错误",
            cancelComplete: "取消订单成功",
            cancelConfirm: "确定要取消订单？本订单如使用了网络优惠券或早餐券，取消后优惠券或早餐券将立即失效且不可恢复。"
        },
        empty: {
            phoneAndCard: "请输入手机号/会员卡号",
            passWord: "请输入密码",
            captcha: "请输入验证码",
            phone: "请输入手机号码",
            name: "请输入姓名",
            sms: "请输入短信验证码"
        },
        validate: {
            rightPassword: "密码长度必须为6-20位",
            rightPhone: "请输入正确的手机号码"
        }
    };


    /* 模块定义 {{{ */
    _n.app('Account', {
        init: function () {
            _vars.errorDiv = $(".errorS");
        },/*登录*/
        LoginAction: function () {
            $("#imgPassword").on("click", function () {
                _func.clearMessage();
                var $self = $(this),
                      $password = $("#passWord");
                if ($self.hasClass('enable')) {
                    $password.attr("type", "password");
                    $self.removeClass("enable");
                } else {
                    $self.addClass("enable");
                    $password.attr("type", "text");
                }
            });
            $("#loginSubmit").on('click', function () {
                var _state = 1;
                _func.clearMessage();
                if (!$(this).get(0).hasAttribute('loading')) {
                    var $userName = $("#userName"),
                        $passWord = $("#passWord"),
                        $sms = $("#codeSms"),
                        $captcha = $("#captcha");
                    var smsLogin = $("#passWord").parent().parent().parent('.password').hasClass('Ldn');
                    var url = $("#returnUrl").data("url");
                    //登陆账号 密码 图形验证码不能为空
                    if (!_func.CheCkEmpty($userName, smsLogin ? "手机号码" : "手机号/会员卡号", _func.showMessage)) {
                        return false;
                    }
                    if (smsLogin) {
                        _state = 2;
                        if (!_func.CheCkEmpty($sms, "动态密码", _func.showMessage)) {
                            return false;
                        }
                    } else {
                        _state = 1;
                        if (!_func.CheCkEmpty($passWord, "密码", _func.showMessage)) {
                            return false;
                        }
                    }
                    //显示图形验证码的情况下，验证码不能为空
                    if ($("#divValidate")[0].style.display != "none" && !_func.CheCkEmpty($captcha, "验证码", _func.showMessage)) {
                        return false;
                    }
                    if (_state == 1) {

                        $.ajax({
                            url: "/Submit/Login" + window.location.search,
                            //method: 'PUT',
                            type: "PUT",
                            data: {
                                Username: $userName.val(),
                                Password: $passWord.val(),
                                ImgCaptcha: $captcha.val(),
                                LoginType: "Personal",
                                __RequestVerificationToken: _vars.token
                            },
                            dataType: 'json',
                            async: false,
                            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                            success: function (data) {
                                if (data.Result) {
                                    _vars.token = data.FormToken;
                                    window.location.href = data.RedirectUrlWithTicket;
                                } else {
                                    _vars.token = data.FormToken;
                                    _func.showMessage(data.Message);
                                }
                            },
                            error: function (data) {
                                _func.showMessage('服务器调用失败');
                            }
                        });
                    } else if (_state == 2) {
                        $.ajax({
                            url: "/Submit/LoginWithCode" + window.location.search,
                            type: 'PUT',
                            data: {
                                Mobile: $userName.val(),
                                Code: $sms.val(),
                                __RequestVerificationToken: _vars.token
                            },
                            dataType: 'json',
                            async: false,
                            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                            success: function (data) {
                                    _vars.token = data.FormToken;
                                    if (data.Result) {
                                        window.location.href = data.RedirectUrlWithTicket;
                                    } else {
                                        _func.showMessage(data.Message);
                                    }
                            },
                            error: function (data) {
                                _func.showMessage('服务器调用失败');
                            }
                        });
                    }
                }
            });
           
            $('.tabs .tab').on('click', function () {
                _func.clearMessage();
                var $tab, active, index, prevActive;
                $tab = $(this);
                if (!$tab.hasClass('active')) {
                    prevActive = $tab.siblings('.active').removeClass('active').data('active');
                    active = $tab.addClass('active').data('active');
                    $tab.parent().removeClass(prevActive).addClass(active);
                    index = $tab.index();
                    $("#userName").attr('placeholder', $("#userName").data(String(index)));
                    $('.tabcontent .item.password').addClass('Ldn').eq(index - 1).removeClass('Ldn');
                    /*控制验证码*/
                    if ($('.tabcontent .item.password').eq(0).hasClass("Ldn")) {
                        $("#divValidate").hide();
                    } else {
                        $("#divValidate").show();
                    }
                } 
            });
            $("#pGetSms").on('click', function () {
                if ($(this).parent().parent().parent().hasClass('Ldn')) {
                    return false;
                }
                _func.clearMessage();
                var $btnSms = $(this),
                      $cellPhone = $("#userName");
                if (!_func.CheCkEmpty($cellPhone, "手机号", _func.showMessage)) return false;
                if (!_func.isCellPhone($.trim($cellPhone.val()))) {
                    _func.showMessage(_vars.message.validate.rightPhone);
                    return false;
                }
                _vars.SendSms.init($btnSms, "/Validate/SendLoginSMS", { Mobile: $cellPhone.val(), __RequestVerificationToken: _vars.token }, undefined, { clear: "获取<br>动态密码" });
            });
        },/*注册*/
        RegiterAction: function () {
            $("#imgPassword").on("click", function () {
                _func.clearMessage();
                var $self = $(this),
                      $password = $("#passWord");
                if ($self.hasClass('enable')) {
                    $password.attr("type", "password");
                    $self.removeClass("enable");
                } else {
                    $self.addClass("enable");
                    $password.attr("type", "text");
                }
            });
            $("#pGetSms").on('click', function () {
                _func.clearMessage();
                var $btnSms = $(this),
                      $cellPhone = $("#cellPhone");
                if (!_func.CheCkEmpty($cellPhone, "手机号", _func.showMessage)) return false;
                if (!_func.isCellPhone($.trim($cellPhone.val()))) {
                    _func.showMessage(_vars.message.validate.rightPhone);
                    return false;
                }
                _vars.SendSms.init($btnSms, "/Validate/SendSMS", { Mobile: $cellPhone.val(), BCode: 'Register', __RequestVerificationToken: _vars.token }, undefined, { clear: "获取验证码" });

            });
            /*验证号码是否可用*/
            $("#cellPhone").blur(function () {
                _func.clearMessage();
                var $cellPhone = $("#cellPhone");
                if (!_func.CheCkEmpty($cellPhone, "手机号", _func.showMessage)) {
                    return false;
                }
                if (!_func.isCellPhone($.trim($cellPhone.val()))) {
                    _func.showMessage(_vars.message.validate.rightPhone);
                    return false;
                }
                _vars.pageMobileValidate = false;
                $.ajax({
                    url: "/Validate/CheckMobile",
                    type: 'POST',
                    data: {
                        Mobile: $cellPhone.val(),
                        __RequestVerificationToken: _vars.token
                    },
                    dataType: 'json',
                    async: true,
                    contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                    success: function (data) {
                            _vars.token = data.FormToken;
                            if (data.Result) {
                                _vars.pageMobileValidate = true;
                               
                            } else {
                                _func.showMessage(data.Message);
                                _vars.pageMobileValidate = false;
                                //$("#pGetSms").attr("disabled", "false");
                            }
                    },
                    error: function (data) {
                        _vars.pageMobileValidate = false;
                    }
                });
            });
            $("#registerSubmit").on('click', function () {
                _func.clearMessage();
                if (!$(this).get(0).hasAttribute('loading')) {
                    var $btnRegister = $(this),
                          $cellPhone = $("#cellPhone"),
                          $smsCaptcha = $("#smsCaptcha"),
                          $userName = $("#userName"),
                          $passWord = $("#passWord"),
                          $captcha = $("#captcha");
                    var url = $("#returnUrl").data("url");
                    if (!_func.CheCkEmpty($cellPhone, "手机号", _func.showMessage) || !_func.CheCkEmpty($smsCaptcha, "短信验证码", _func.showMessage) || !_func.CheCkEmpty($userName, "姓名", _func.showMessage) || !_func.CheCkEmpty($passWord, "密码", _func.showMessage)) {
                        return false;
                    }
                    //显示图形验证码的情况下，验证码不能为空
                    if ($("#divValidate")[0].style.display != "none" && !_func.CheCkEmpty($captcha, "验证码", _func.showMessage)) {
                        return false;
                    }
                    if (!_func.isCellPhone($.trim($cellPhone.val()))) {
                        _func.showMessage(_vars.message.validate.rightPhone);
                        return false;
                    }
                    if (!_func.checkIsName($.trim($userName.val()))) {
                        _func.showMessage(_vars.message.register.errorName);
                        return false;
                    }

                    if (!_func.isPassword($.trim($passWord.val()))) {
                        _func.showMessage(_vars.message.validate.rightPassword);
                        return false;
                    }
                    dataMap = {
                        Username: $userName.val(),
                        Password: $passWord.val(),
                        Mobile: $cellPhone.val(),
                        Code: $smsCaptcha.val(),
                        __RequestVerificationToken: _vars.token
                    }
                    // $(this).attr("loading", "loading");
                    if (_vars.pageMobileValidate == true) {

                        $('#registerSubmit').html('正在登陆中');
                        $.ajax({
                            url: "/Submit/SignUp",
                            type: 'PUT',
                            data: dataMap,
                            dataType: 'json',
                            async: true,
                            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                            success: function (data) {
                                _vars.token = data.FormToken;
                                if (data.Result) {
                                    window.location.href = data.RedirectUrlWithTicket;
                                } else {
                                    $('#registerSubmit').html('立即注册');
                                    _func.showMessage(data.Message);
                                }
                            },
                            error: function (data) {
                                $('#registerSubmit').html('立即注册');
                                _vars.pageMobileValidate = false;
                            }
                        });
                    } else {
                        _func.showMessage("该号码不可用");
                    }
                }
            });
        },/*忘记密码*/
        ForgetPasswordAction: function () {
            $("#pGetSms").on('click', function () {
                _func.clearMessage();
                var $btnSms = $(this),
                      $cellPhone = $("#cellPhone");
                if (!_func.CheCkEmpty($cellPhone, "手机号", _func.showMessage)) return false;
                if (!_func.isCellPhone($.trim($cellPhone.val()))) {
                    _func.showMessage(_vars.message.validate.rightPhone);
                    return false;
                }

                _vars.SendSms.init($btnSms, "/Validate/SendSMS", { Mobile: $cellPhone.val(), BCode: 'ResetPassword', __RequestVerificationToken: _vars.token }, undefined, { clear: "获取验证码" });
                //_func.prePostAsync(function () {
                //    _vars.SendSms.init($btnSms, { "CellPhone": encryptedString(_vars.rsakey, $.trim($cellPhone.val()) + "#" + _vars.timestamp), "CaptchaType": "ForgetPassword" });
                //});
            });
            $("#postData").on('click', function () {
                _func.clearMessage();
                if (!$(this).get(0).hasAttribute('loading')) {
                    var $cellPhone = $("#cellPhone"),
                          $sms = $("#smsCaptcha"),
                          $captcha = $("#captcha"),
                          $btnForgetPass = $(this);
                    if (!_func.CheCkEmpty($cellPhone, "手机号", _func.showMessage) || !_func.CheCkEmpty($sms, "短信验证码", _func.showMessage)) {
                        return false;
                    }
                    if ($("#divValidate")[0].style.display != "none" && !_func.CheCkEmpty($captcha, "图形验证码", _func.showMessage)) {
                        return false;
                    }
                    if (!_func.isCellPhone($.trim($cellPhone.val()))) {
                        _func.showMessage(_vars.message.validate.rightPhone);
                        return false;
                    }
                    //$(this).attr("loading", "loading");
                    //_func.prePostAsync(function () {
                    //    _func.checkCellPhoneAndSMS($btnForgetPass, { "CellPhone": encryptedString(_vars.rsakey, $.trim($cellPhone.val()) + "#" + _vars.timestamp), "Captcha": $.trim($sms.val()), "ImgCaptchaCode": $.trim($captcha.val()) });
                    //});
                    $.ajax({
                        url: "/Validate/ValidateSMSCodeAndImgCaptcha",
                        method: 'POST',
                        data: {
                            mobile: $cellPhone.val(),
                            smsCode: $sms.val(),
                            imgCode: $captcha.val(),
                            BCode: 'ResetPassword',
                            __RequestVerificationToken: _vars.token
                        },
                        dataType: 'json',
                        async: true,
                        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                        success: function (data) {
                                _vars.token = data.FormToken;
                                if (data.Result) {
                                    $("#FContent").hide();
                                    $("#SContent").show();
                                } else {
                                    _func.showMessage(data.Message);
                                    $("#FContent").show();
                                    $("#SContent").hide();
                                }
                        },
                        error: function (data) {
                            _func.showMessage("服务器调用失败");
                            $("#FContent").show();
                            $("#SContent").hide();
                        }
                    });
                }
            });
            //
            $("#imgPasswordo").on("click", function () {
                _func.clearMessage();
                var $self = $(this),
                      $password = $("#passWordO");
                if ($self.hasClass('enable')) {
                    $password.attr("type", "password");
                    $self.removeClass("enable");
                } else {
                    $self.addClass("enable");
                    $password.attr("type", "text");
                }
            });
            $("#imgPasswords").on("click", function () {
                _func.clearMessage();
                var $self = $(this),
                      $password = $("#passWordS");
                if ($self.hasClass('enable')) {
                    $password.attr("type", "password");
                    $self.removeClass("enable");
                } else {
                    $self.addClass("enable");
                    $password.attr("type", "text");
                }
            });
            /*确认修改提交*/
            $("#rePasswordSubmit").on('click', function () {
                _func.clearMessage();
                if (!$(this).get(0).hasAttribute('loading')) {
                    var $cellPhone = $("#cellPhone"),
                        $password = $("#passWordO"),
                        $passwordS = $("#passWordS");
                    if (!_func.CheCkEmpty($password, "密码", _func.showMessage) || !_func.CheCkEmpty($passwordS, "重复密码", _func.showMessage)) {
                        return false;
                    }
                    if (!_func.isPassword($.trim($password.val())) || !_func.isPassword($.trim($passwordS.val()))) {
                        _func.showMessage(_vars.message.validate.rightPassword);
                        return false;
                    }
                    if ($.trim($password.val()) != $.trim($passwordS.val())) {
                        _func.showMessage("两次输入密码不一致");
                        return false;
                    }
                    $.ajax({
                        url: "/Submit/ResetPassword" + window.location.search,
                        type: 'PUT',
                        data: {
                            mobile: $cellPhone.val(),
                            pwd: $password.val(),
                            type: 'PERSONAL',
                            __RequestVerificationToken: _vars.token
                        },
                        dataType: 'json',
                        async: true,
                        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                        success: function (data) {
                                _vars.token = data.FormToken;
                                if (data.Result) {
                                    $("#FContent").hide();$("#SContent").hide(); $("#TContent").show();
                                } else {
                                    _func.showMessage(data.Message);
                                    $("#FContent").hide(); $("#SContent").show(); $("#TContent").hide();
                                }
                        },
                        error: function (data) {
                            _func.showMessage('服务器调用失败');
                            $("#FContent").hide(); $("#SContent").show(); $("#TContent").hide();
                        }
                    });
                    // $(this).attr("loading", "loading");
                };
            });

        },
        //MyCenterAction: function () {
        //    $("#loginout").on('click', function () {
        //        _func.clearMessage();
        //        _func.loginOut();
        //    });
        //},
    });
    /* }}} */
    return _n;
}).call(this.N = this.ndoo = this.ndoo || {}, Zepto);

