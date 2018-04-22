$(function () {

    $('.edit-mode').hide();
    $('.edit-user, .cancel-user').on('click', function () {

        var tr = $(this).parents('tr:first');
       
        tr.find('.edit-mode, .display-mode').toggle();
    });

    $('.delete-user').on('click', function () {
        var tr = $(this).parents('tr:first');
        var mapid = tr.find("#MapIDid").val();
        var StudyStatusesModel = {
            "mapID": mapid
        };

        $.ajax({
            url: '/DataMapping/DeleteMap/',
            data: JSON.stringify(StudyStatusesModel),
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                alert(data);
                tr.remove();
            },
            error: function () {
                alert('Delete Unsucessful');
            }
        });

    });

    $('.save-user').on('click', function () {
        var tr = $(this).parents('tr:first');
        var dtSrcStatus = tr.find("#lblDataSourceStudyStatus").html();
        var dtDrc = tr.find("#lblDataSource").html();
        var localStatus = tr.find("#txtLocalStudyStatus").val();
        var mapid = tr.find("#MapIDid").val();
        tr.find("#spanLocalStudyStatus").html(localStatus);
        tr.find('.edit-mode, .display-mode').toggle();

        var StudyStatusesModel = {
            "MapID": mapid,
            "datasource": dtDrc,
            "dataSourceStatus": dtSrcStatus,
            "localStatus": localStatus
        };


        $.ajax({
            url: '/DataMapping/UpdateStatusMap/',
            data: JSON.stringify(StudyStatusesModel),
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                alert(data);
            },
            error: function () {
                alert('Update Unsucessful');
            }
        });
    });

    $("#addbtn").click(function () {
        var un = $("#groupID").val();

        $.ajax({
            type: "POST",
            url: '/DataMapping/RenderMapper',
            data: JSON.stringify({
                groupID: un
            }),
            contentType: 'application/json',
            success: function (data) {

                $("#statusmapform").html(data);
            },
            error: function () {
                alert('Error post');
            }
        });

    });
});