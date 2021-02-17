function Reply(commentID) {
    if (document.getElementsByClassName("visable-comment")[0] != null) {
        document.getElementsByClassName("visable-comment")[0].classList.add("d-none");
        document.getElementsByClassName("visable-comment")[0].classList.remove("visable-comment");
    }  
    document.getElementById("Reply" + commentID).classList.remove("d-none");
    document.getElementById("Reply" + commentID).classList.add("visable-comment");
}