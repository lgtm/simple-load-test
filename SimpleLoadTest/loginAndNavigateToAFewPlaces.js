var casper = require("casper").create();

casper.start("https://admin.staging.groupcommerce.com", function(){
    casper.evaluate(function(username, password) {
        $('#Username').val(username);
        $('#Password').val(password);
        $('input[type="submit"]').click();
    }, casper.cli.get(0), casper.cli.get(1));
});


casper.waitForSelector("a[href='/account/impersonate']",
    function(){
        casper.click("a[href='/account/impersonate']");
        casper.waitForSelector('#UserKey', function(){
                casper.evaluate(function(userKey){
                    $("#UserKey").val(userKey);
                    $('input[type="submit"]').click();
                }, casper.cli.get(2));


            }, function(){
                casper.capture('impersonatePageDidNotLoad.png');
                this.test.fail('Could not impersonate user ' + userName);
            },
            1000);
    },
    function(){
        casper.capture('impersonateLinkMissing.png');
        this.test.fail('the impersonate user link was never available')
    },
    1000);





for (var i = 0; i < 25; i++) {
	casper.thenClick("a[href='/merchantcontract']", function(){
		casper.echo('on the contract grid');
	});
	casper.thenClick("a[href='/offer']", function(){
		casper.echo('on the offer grid');
	});
	casper.thenClick("a[href='/merchant']", function(){
		casper.echo('on the merchant grid');
	});
	casper.thenClick("a[href='/schedule']", function(){
		casper.echo('on the calendar');
	});
	casper.thenClick("a[href='/home']", function(){
		casper.echo('went home');
	});
}



casper.thenClick("a[href='/account/logoff']", function(){
    casper.echo('logged off');
});

casper.run(function() {
 this.test.done(); 
 casper.exit();
});

