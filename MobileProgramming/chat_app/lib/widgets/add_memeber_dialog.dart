import 'package:chat_app/chat_service.dart';
import 'package:flutter/material.dart';
import 'package:mobile_scanner/mobile_scanner.dart';
import 'package:provider/provider.dart';

import '../models/channel.dart';

class AddMemberDialog extends StatefulWidget {
  const AddMemberDialog({
    super.key,
    required this.channel,
  });

  final Channel channel;

  @override
  State<AddMemberDialog> createState() => _AddMemberDialogState();
}

class _AddMemberDialogState extends State<AddMemberDialog> {
  final cameraController = MobileScannerController();
  @override
  void dispose() {
    cameraController.dispose();
    super.dispose();
  }
  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      title: const Text('Add User'),
      content: SizedBox(
        width: 300,
        height: 300,
        child: MobileScanner(fit: BoxFit.contain, controller: cameraController, onDetect: _onScannerDetect,)
      ),
      actions: [
        TextButton(
          child: const Text('Cancel'),
          onPressed: () {
            Navigator.of(context).pop();
          },
        ),
      ],
    );
  }

  void _onScannerDetect(BarcodeCapture barcodes) {
    try {
    final barcode = barcodes.barcodes.firstWhere((code) => code.rawValue?.length == 28);
    final chat = Provider.of<ChatService>(context, listen: false);
    chat.addMember(widget.channel, barcode.rawValue!);
    Navigator.of(context).pop();
    } on StateError {}
  }
}
