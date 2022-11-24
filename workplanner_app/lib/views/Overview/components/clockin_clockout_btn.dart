import 'package:flutter/material.dart';

import '../../../constants.dart';
import '../../Calender/calender_page.dart';
import '../../Login/login_screen.dart';

class ClockInAndClockOutBtn extends StatelessWidget {
  const ClockInAndClockOutBtn({
    Key? key,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Hero(
          tag: "clockin_btn",
          child: ElevatedButton(
            onPressed: () {},
            style:
                ElevatedButton.styleFrom(primary: kPrimaryColor, elevation: 0),
            child: Text("Clock In".toUpperCase(),
                style: const TextStyle(
                    color: Colors.black, fontWeight: FontWeight.bold)),
          ),
        ),
        const SizedBox(height: 16),
        ElevatedButton(
          onPressed: () {},
          style: ElevatedButton.styleFrom(primary: kPrimaryColor, elevation: 0),
          child: Text(
            "Clock Out".toUpperCase(),
            style: const TextStyle(
                color: Colors.black, fontWeight: FontWeight.bold),
          ),
        ),
        const SizedBox(height: 16),
        ElevatedButton(
          onPressed: () {
            Navigator.push(
              context,
              MaterialPageRoute(
                builder: (context) {
                  return const CalenderPage();
                },
              ),
            );
          },
          style: ElevatedButton.styleFrom(primary: kPrimaryColor, elevation: 0),
          child: Text("Calender View".toUpperCase(),
              style: const TextStyle(
                  color: Colors.black, fontWeight: FontWeight.bold)),
        ),
      ],
    );
  }
}
