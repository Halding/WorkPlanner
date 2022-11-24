import 'package:flutter/material.dart';
import 'package:workplanner_app/views/Calender/calender_page.dart';

import '../../components/background.dart';
import '../../responsive.dart';
import 'components/clockin_clockout_btn.dart';
import 'components/overview_page_top.dart';

class OverviewPage extends StatelessWidget {
  const OverviewPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Background(
      child: SingleChildScrollView(
        child: SafeArea(
          child: Responsive(
            desktop: Row(
              mainAxisAlignment: MainAxisAlignment.end,
              children: [
                const Expanded(
                  child: OverviewTopPage(),
                ),
                Expanded(
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: const [
                      SizedBox(
                        width: 450,
                        child: ClockInAndClockOutBtn(),
                      ),
                    ],
                  ),
                ),
              ],
            ),
            mobile: const MobileOverviewPage(),
          ),
        ),
      ),
    );
  }
}

class MobileOverviewPage extends StatelessWidget {
  const MobileOverviewPage({
    Key? key,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisAlignment: MainAxisAlignment.center,
      children: <Widget>[
        const OverviewTopPage(),
        Row(
          children: const [
            Spacer(),
            Expanded(
              flex: 8,
              child: ClockInAndClockOutBtn(),
            ),
            Spacer(),
          ],
        ),
      ],
    );
  }
}
