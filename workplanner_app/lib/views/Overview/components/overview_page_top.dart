import 'package:flutter/material.dart';

class OverviewTopPage extends StatelessWidget {
  const OverviewTopPage({
    Key? key,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Column(
      children: const [
        Text(
          "Welcome Svend",
          style: TextStyle(
              fontWeight: FontWeight.bold,
              fontSize: 20,
              color: Color.fromARGB(255, 0, 0, 0)),
        ),
        Text(
          "Clock in/out or go to Calender",
          style: TextStyle(
              fontWeight: FontWeight.bold,
              fontSize: 25,
              color: Color.fromARGB(255, 0, 0, 0)),
        ),
        Padding(padding: EdgeInsets.fromLTRB(0, 0, 0, 75)),
      ],
    );
  }
}
