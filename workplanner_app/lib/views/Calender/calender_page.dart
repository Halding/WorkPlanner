import 'package:flutter/material.dart';
import 'package:syncfusion_flutter_calendar/calendar.dart';

import '../../models/post.dart';
import '../../models/shift.dart';
import '../../services/remote_service.dart';

class CalenderPage extends StatefulWidget {
  const CalenderPage({Key? key}) : super(key: key);

  @override
  State<CalenderPage> createState() => _CalenderPageState();
}

class _CalenderPageState extends State<CalenderPage> {
  List<Post>? posts;
  List<Shift>? shifts;
  var isLoadedPost = false;
  var isLoadedShift = false;
  var isLoadedShiftFile = false;

  @override
  void initState() {
    super.initState();
    getDataShiftFile();
  }

  getDataPost() async {
    posts = await RemoteService().getPosts();
    if (posts != null) {
      setState(() {
        isLoadedPost = true;
      });
    }
  }

  getDataShift() async {
    shifts = await RemoteService().getShift();
    if (shifts != null) {
      setState(() {
        isLoadedShift = true;
      });
    }
  }

  getDataShiftFile() async {
    shifts = await RemoteService().readShiftJson();
    if (shifts != null) {
      setState(() {
        isLoadedShiftFile = true;
      });
    }
  }

  List<Appointment> getAppointments() {
    List<Appointment> meetings = <Appointment>[];
    final DateTime today = DateTime.now();
    final DateTime startTime =
        DateTime(today.year, today.month, today.day, 9, 0, 0);
    final DateTime endTime = startTime.add(const Duration(hours: 2));

    if (shifts != null) {
      for (var shift in shifts!) {
        meetings.add(Appointment(
            startTime: shift.startTime,
            endTime: shift.endTime,
            subject: "${shift.employeeFirstName}",
            color: Color(0xFF2196F3)));
      }
    }

    return meetings;
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text("My Shift")),
      body: SfCalendar(
        view: CalendarView.week,
        firstDayOfWeek: 1,
        dataSource: MeetingDataScource(getAppointments()),
      ),
    );
  }
}

class MeetingDataScource extends CalendarDataSource {
  MeetingDataScource(List<Appointment> source) {
    appointments = source;
  }
}
