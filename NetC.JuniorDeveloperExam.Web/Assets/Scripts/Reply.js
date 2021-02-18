function jsCheckDisabled() {
    //Hide warning message if JS not disabled
    var jsDisabledShow = document.getElementsByClassName("jsDisabledShow");
    if (jsDisabledShow != null) {
        for (var i = jsDisabledShow.length - 1; i >= 0; i--) {
            jsDisabledShow[i].classList.remove("jsDisabledShow")
        }
    }
    //Show reply buttons if JS not diabled
    var jsDisabledHide = document.getElementsByClassName("jsDisabledHide");
    if (jsDisabledHide != null) {
        for (var i = jsDisabledHide.length -1; i >= 0 ; i--) {
            jsDisabledHide[i].classList.remove("jsDisabledHide")
        }
    }
};

function Reply(commentID) {
    //If a reply section is active, hide it and show its show button
    if (document.getElementsByClassName("activeReply")[0] != null) {
        //Reply
        document.getElementsByClassName("activeReply")[0].classList.remove("activeReply");
        //ReplyBtn
        document.getElementsByClassName("activeReplyBtn")[0].classList.remove("activeReplyBtn");
    }

    //For the selected reply, show it and hide its show button
    //Reply
    document.getElementById("Reply" + commentID).classList.add("activeReply");
    //ReplyBtn
    document.getElementById("ReplyBtn" + commentID).classList.add("activeReplyBtn");
}
